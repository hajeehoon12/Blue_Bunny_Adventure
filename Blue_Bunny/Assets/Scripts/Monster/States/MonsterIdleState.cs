using System.Collections;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    private Vector3 idleMoveDirection;
    private Coroutine directionCoroutine;

    public MonsterIdleState(MonsterStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        /*Debug.Log("MonsterIdleState::Enter()");*/

        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        directionCoroutine = stateMachine.Monster.StartCoroutine(SetDirectionCoroutine());
    }

    public override void Exit()
    {
        /*Debug.Log("MonsterIdleState::Exit()");*/

        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        stateMachine.Monster.StopCoroutine(directionCoroutine); 
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        UpdateIdleMove();
    }

    private void UpdateIdleMove()
    {
        // 좌우 몬스터
        if (stateMachine.Monster.Data.MonsterType == MonsterType.Horizontal)
        {
            // 이동방향 레이가 땅에 안 닿으면 반대 방향으로 가기
            if (IsRayHitGround(1, idleMoveDirection, Vector3.down, Color.green) == false || 
                IsRayHitGround(1.5f, new Vector3(-0.5f, 0,0), new Vector3(0.7f, 0, 0), Color.black))
            {
                idleMoveDirection *= -1;
                RotateSprite(idleMoveDirection);
            }

            stateMachine.Monster.transform.position += idleMoveDirection * stateMachine.Monster.Data.IdleSpeed * Time.deltaTime;
        }
        // 상하 몬스터
        else if (stateMachine.Monster.Data.MonsterType == MonsterType.Vertical)
        {
            // 땅 위면 방향 뒤집기
            if (IsRayHitGround(0.7f, Vector3.zero, Vector3.down, Color.yellow) == true)
            {
                idleMoveDirection *= -1;
            }

            // 너무 위에 있으면 방향 바꾸기
            if (IsRayHitGround(stateMachine.Monster.Data.IdleFlyDistance, Vector3.zero, Vector3.down, Color.green) == false)
            {
                idleMoveDirection = new Vector3(0, -1, 0);
            }
            stateMachine.Monster.transform.position += idleMoveDirection * stateMachine.Monster.Data.IdleSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// 1초마다 방향을 바꾸는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetDirectionCoroutine()
    {
        while (true)
        {
            int randomValue = Random.Range(-1, 2);

            if (stateMachine.Monster.Data.MonsterType == MonsterType.Horizontal)
            {
                idleMoveDirection = new Vector3(randomValue, 0, 0);
                RotateSprite(idleMoveDirection);
            }
            else
            {
                idleMoveDirection = new Vector3(0, randomValue, 0);
            }

            yield return new WaitForSeconds(stateMachine.Monster.Data.IdleChangeDirectionSecond);
        }
    }
}
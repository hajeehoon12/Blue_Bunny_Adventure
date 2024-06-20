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
        Debug.Log("MonsterIdleState::Enter()");

        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        directionCoroutine = stateMachine.Monster.StartCoroutine(SetDirectionCoroutine()); // �ڷ�ƾ ����
    }

    public override void Exit()
    {
        Debug.Log("MonsterIdleState::Exit()");

        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
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
        stateMachine.Monster.transform.position += idleMoveDirection * stateMachine.Monster.Data.IdleSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 5�ʸ��� ������ �ٲٴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetDirectionCoroutine()
    {
        while (true)
        {
            int randomValue = Random.Range(-1, 2);

            if(stateMachine.Monster.Data.MonsterType == MonsterType.Horizontal)
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

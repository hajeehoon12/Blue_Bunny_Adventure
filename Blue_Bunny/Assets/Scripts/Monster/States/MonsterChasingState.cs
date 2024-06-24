using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterChasingState : MonsterBaseState
{
    public MonsterChasingState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        /*Debug.Log("MonsterChasingState::Enter()");*/
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.ChasingParameterHash);
    }

    public override void Exit()
    {
        /*Debug.Log("MonsterChasingState::Exit()");*/
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.ChasingParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        UpdateChasingDirection();
        UpdateChasingMove();
    }
    private void UpdateChasingMove()
    {
        // 가로 몬스터 -> 좌우로 이동
        if (stateMachine.Monster.Data.MonsterType == MonsterType.Horizontal)
        {
            Vector3 moveDirection = new Vector3(stateMachine.MovementDirection.x, 0, 0);

            // 이동방향 레이가 땅에 닿아있을 때만 앞으로 가기
            if (IsRayHitGround(1, moveDirection, Vector3.down, Color.red) == true &&
                IsRayHitGround(1.5f, new Vector3(-0.5f, 0, 0), new Vector3(0.7f, 0, 0), Color.black) == false)
            {
                stateMachine.Monster.transform.position += moveDirection * stateMachine.Monster.Data.ChasingSpeed * Time.deltaTime;
            }
        }
        // 세로 몬스터 -> 4방향으로 이동
        else if (stateMachine.Monster.Data.MonsterType == MonsterType.Vertical)
        {
            stateMachine.Monster.transform.position += stateMachine.MovementDirection * stateMachine.Monster.Data.ChasingSpeed * Time.deltaTime;
        }
    }

    protected void UpdateChasingDirection()
    {
        stateMachine.MovementDirection = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).normalized;
        RotateSprite(stateMachine.MovementDirection);
    }
}

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
        Debug.Log("MonsterChasingState::Enter()");
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.ChasingParameterHash);
    }

    public override void Exit()
    {
        Debug.Log("MonsterChasingState::Exit()");
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

        UpdateDirection();
        UpdateChasingMove();
    }
    private void UpdateChasingMove()
    {
        stateMachine.Monster.transform.position += stateMachine.MovementDirection * stateMachine.Monster.Data.BaseSpeed * Time.deltaTime;
    }
}

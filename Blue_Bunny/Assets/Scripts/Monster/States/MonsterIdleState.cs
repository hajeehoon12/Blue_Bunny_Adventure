using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("MonsterIdleState::Enter()");

        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
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

    }
}

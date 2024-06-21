using UnityEngine;

public class MonsterDeadState : MonsterBaseState
{
    public MonsterDeadState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("MonsterDeadState::Enter()");
        StartAnimation(stateMachine.Monster.AnimationData.DeadParameterHash);

        base.Enter();
    }

    public override void Exit()
    {
        Debug.Log("MonsterDeadState::Exit()");
        StopAnimation(stateMachine.Monster.AnimationData.DeadParameterHash);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "MonsterDead");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            stateMachine.Monster.gameObject.SetActive(false);
        }
    }
}
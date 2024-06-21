using UnityEngine;

public class MonsterGetHitState : MonsterBaseState
{
    public MonsterGetHitState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        /*Debug.Log("MonsterGetHitState::Enter()");*/
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.GetHitParameterHash);
    }

    public override void Exit()
    {
        /*Debug.Log("MonsterGetHitState::Exit()");*/
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.GetHitParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "MonsterGetHit");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

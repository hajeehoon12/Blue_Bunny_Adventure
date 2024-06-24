using UnityEngine;

public class MonsterDeadState : MonsterBaseState
{
    public MonsterDeadState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        /*Debug.Log("MonsterDeadState::Enter()");*/
        StartAnimation(stateMachine.Monster.AnimationData.DeadParameterHash);

        stateMachine.Monster.BoxCollider2D.enabled = false;
        GameManager.Instance.spawnManager.ApplyAliveMonsterDeath(stateMachine.Monster.Data.MonsterType);

        base.Enter();
    }

    public override void Exit()
    {
        /*Debug.Log("MonsterDeadState::Exit()");*/
        StopAnimation(stateMachine.Monster.AnimationData.DeadParameterHash);
        stateMachine.Monster.BoxCollider2D.enabled = true;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "MonsterDead");
        if (normalizedTime >= 1f)
        {
            stateMachine.Monster.gameObject.SetActive(false);
        }
    }
}
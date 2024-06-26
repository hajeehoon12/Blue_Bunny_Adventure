using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonsterBaseState 상속받은 상태 클래스 일단 다 갖고 있기
/// 타겟 플레이어만 있다.
/// </summary>
public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; private set; }
    public GameObject Target { get; private set; }
    public Vector3 MovementDirection { get; set; }

    public MonsterIdleState IdleState { get; }
    public MonsterChasingState ChasingState { get; }
    public MonsterGetHitState GetHitState { get; }
    public MonsterDeadState DeadState { get; }

    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;

        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChasingState(this);
        GetHitState = new MonsterGetHitState(this);
        DeadState = new MonsterDeadState(this);

        Target = FindTarget();
    }

    public GameObject FindTarget()
    {
        Target = GameObject.FindGameObjectWithTag(Define.PLAYER_TAG);
        if(Target == null)
        {
            /*Debug.Log($"MonsterStateMachine::FindTarget() Target is null");*/
            return null;
        }

        /*Debug.Log($"Target : {Target.name}");*/
        return Target;
    }
}
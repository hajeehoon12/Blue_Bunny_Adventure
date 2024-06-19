using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
}

public class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; private set; }
    public GameObject Target { get; private set; }
    public Vector3 MovementDirection { get; set; }

    public MonsterIdleState IdleState { get; }
    public MonsterChasingState ChasingState { get; }

    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;

        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChasingState(this);

        Target = FindTarget();
    }

    public GameObject FindTarget()
    {
        Target = GameObject.FindGameObjectWithTag(Define.PLAYER_TAG);
        if(Target == null)
        {
            Debug.Log($"MonsterStateMachine::FindTarget() Target is null");
            return null;
        }

        Debug.Log($"Target : {Target.name}");
        return Target;
    }
}
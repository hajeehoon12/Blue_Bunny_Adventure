using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
}

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }

    /// <summary>
    /// 애니메이터의 상태 정보를 가져와서, 주어진 태그에 해당하는 상태의 진행 시간을 반환합니다.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        // 현재 애니메이터의 상태 정보를 가져옵니다.
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // 애니메이터가 전환 중인지 확인하고, 전환 중이라면 다음 상태의 정보를 확인합니다.
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        // 애니메이터가 전환 중이 아니고, 현재 상태가 주어진 태그와 일치하는지 확인합니다.
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        // 위의 조건에 모두 해당되지 않으면, 진행 시간을 0으로 반환합니다.
        else
        {
            return 0f;
        }
    }

    protected bool IsInChasingRange()
    {
        if(stateMachine.Target == null)
        {
            /*Debug.Log($"MonsterBaseState::IsInChasingRange() : Target is null");*/

            return false;
        }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.ChasingRange * stateMachine.Monster.Data.ChasingRange;
    }

    protected void RotateSprite(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        stateMachine.Monster.SpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;

        /*Debug.Log($"MonsterBaseState::RotateSprite()");*/
    }

    protected bool IsRayHitGround(float distance, Vector3 offset, Vector3 toward, Color color)
    {
        Debug.DrawRay(stateMachine.Monster.transform.position + offset, toward * distance, color);
        RaycastHit2D rayHit = Physics2D.Raycast(stateMachine.Monster.transform.position + offset, toward, distance, LayerMask.GetMask(Define.GROUND_Layer));
        if (rayHit.collider == null)
        {
            return false;
        }
        return true;
    }
}
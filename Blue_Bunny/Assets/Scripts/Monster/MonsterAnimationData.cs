using UnityEngine;

[SerializeField]
public class MonsterAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
    }
}

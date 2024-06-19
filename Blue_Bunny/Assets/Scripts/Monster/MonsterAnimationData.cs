using UnityEngine;

[SerializeField]
public class MonsterAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Chasing";

    public int IdleParameterHash { get; private set; }
    public int ChasingParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChasingParameterHash = Animator.StringToHash(walkParameterName);
    }
}

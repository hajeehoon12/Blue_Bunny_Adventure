using UnityEngine;

[SerializeField]
public class MonsterAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Chasing";
    [SerializeField] private string getHitParameterName = "GetHit";


    public int IdleParameterHash { get; private set; }
    public int ChasingParameterHash { get; private set; }
    public int GetHitParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChasingParameterHash = Animator.StringToHash(walkParameterName);
        GetHitParameterHash = Animator.StringToHash(getHitParameterName);
    }
}

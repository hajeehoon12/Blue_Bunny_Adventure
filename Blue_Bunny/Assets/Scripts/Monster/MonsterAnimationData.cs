using UnityEngine;

/// <summary>
/// Monster의 애니메이션 파라미터 이름을 해시로 저장하는 클래스
/// </summary>
[SerializeField]
public class MonsterAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Chasing";
    [SerializeField] private string getHitParameterName = "GetHit";
    [SerializeField] private string deadParameterName = "Dead";

    public int IdleParameterHash { get; private set; }
    public int ChasingParameterHash { get; private set; }
    public int GetHitParameterHash { get; private set; }
    public int DeadParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChasingParameterHash = Animator.StringToHash(walkParameterName);
        GetHitParameterHash = Animator.StringToHash(getHitParameterName);
        DeadParameterHash = Animator.StringToHash(deadParameterName);
    }
}

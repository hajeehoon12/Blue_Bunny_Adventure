using UnityEngine.UI;

public class HPBar : SlideBar
{
    public Image BackgroundBar;

    void Start()
    {
        UpdateBar_Add(0.0f);
    }

    //체력이 찰 때, 호출하는 함수
    public override void UpdateBar_Add(float amount)
    {
        base.UpdateBar_Add(amount);
    }

    //체력이 빠질 때, 호출하는 함수
    public override void UpdateBar_Sub(float amount)
    {
        base.UpdateBar_Sub(amount);
    }
}

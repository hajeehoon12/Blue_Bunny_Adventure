using UnityEngine;
using UnityEngine.UI;

public class MPBar : SlideBar
{
    public Image BackgroundBar;

    void Start()
    {
        UpdateBar_Add();
    }

    //마나가 찰 때, 호출하는 함수
    public override void UpdateBar_Add()
    {
        base.UpdateBar_Add();
    }

    //마나가 빠질 때, 호출하는 함수
    public override void UpdateBar_Sub()
    {
        base.UpdateBar_Sub();
    }

    public void ChangeBGAlpha(float _alpha)
    {
        BackgroundBar.color = new Color(BackgroundBar.color.r, BackgroundBar.color.g, BackgroundBar.color.b, _alpha);
    }
}

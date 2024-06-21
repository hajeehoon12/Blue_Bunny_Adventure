using UnityEngine.UI;

public class MPBar : SlideBar
{
    public Image BackgroundBar;

    void Start()
    {
        UpdateBar_Add();
    }

    //마나가 찰 때, 호출하는 함수 ( 매개변수는 절대값을 넣어준다. )
    //public override void UpdateBar_Add(float amount)
    //{
    //    base.UpdateBar_Add(amount);
    //}

    ////마나가 빠질 때, 호출하는 함수 ( 매개변수는 절대값을 넣어준다. )
    //public override void UpdateBar_Sub(float amount)
    //{
    //    base.UpdateBar_Sub(amount);
    //}
    public override void UpdateBar_Add()
    {
        base.UpdateBar_Add();
    }

    //마나가 빠질 때, 호출하는 함수 ( 매개변수는 절대값을 넣어준다. )
    public override void UpdateBar_Sub()
    {
        base.UpdateBar_Sub();
    }
}

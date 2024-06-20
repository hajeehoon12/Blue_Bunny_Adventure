using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : SlideBar
{
    void Start()
    {
        UpdateBar_Add(0.0f);
    }

    void Update()
    {
        
    }

    //마나가 찰 때, 호출하는 함수 ( 매개변수는 절대값을 넣어준다. )
    public override void UpdateBar_Add(float amount)
    {
        base.UpdateBar_Add(amount);
    }

    //마나가 빠질 때, 호출하는 함수 ( 매개변수는 절대값을 넣어준다. )
    public override void UpdateBar_Sub(float amount)
    {
        base.UpdateBar_Sub(amount);
    }
}

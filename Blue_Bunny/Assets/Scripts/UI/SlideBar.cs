using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface ISlideBar
{
    //void UpdateBar_Add(float amount);
    //void UpdateBar_Sub(float amount);
    void UpdateBar_Add();
    void UpdateBar_Sub();
    void CheckZeroOrMax();
    void SetPercent();
}

public class SlideBar : MonoBehaviour, ISlideBar
{
    public float Max;
    public float Current;
    public Image SlideBarImage;

    //public virtual void UpdateBar_Add(float amount)
    //{
    //    //Current += amount;
    //    CheckZeroOrMax();
    //    SetPercent();
    //}
    //public virtual void UpdateBar_Sub(float amount)
    //{
    //    //Current -= amount;
    //    CheckZeroOrMax();
    //    SetPercent();
    //}
    public virtual void UpdateBar_Add()
    {
        //Current += amount;
        CheckZeroOrMax();
        SetPercent();
    }
    public virtual void UpdateBar_Sub()
    {
        //Current -= amount;
        CheckZeroOrMax();
        SetPercent();
    }
    public virtual void CheckZeroOrMax()
    {
        Current = Current < 0.0f ? 0.0f : Current;
        Current = Current > Max ? Max : Current;
    }
    public virtual void SetPercent()
    {
        if (Current <= 0.0f) SlideBarImage.fillAmount = 0.0f;
        else SlideBarImage.fillAmount = Current / Max;
    }

    public void ChangeBarAlpha(float _alpha)
    {
        SlideBarImage.color = new Color(SlideBarImage.color.r, SlideBarImage.color.g, SlideBarImage.color.b, _alpha);
    }
}

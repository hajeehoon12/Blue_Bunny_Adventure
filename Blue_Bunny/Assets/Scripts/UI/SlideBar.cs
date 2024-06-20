using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ISlideBar
{
    void UpdateBar_Add(float amount);
    void UpdateBar_Sub(float amount);
    void CheckZeroOrMax();
}

public class SlideBar : MonoBehaviour, ISlideBar
{
    public float Max;
    public float Current;

    public virtual void UpdateBar_Add(float amount)
    {
        Current += amount;
        CheckZeroOrMax();
    }
    public virtual void UpdateBar_Sub(float amount)
    {
        Current -= amount;
        CheckZeroOrMax();
    }
    public virtual void CheckZeroOrMax()
    {
        Current = Current < 0.0f ? 0.0f : Current;
        Current = Current > Max ? Max : Current;
    }
}

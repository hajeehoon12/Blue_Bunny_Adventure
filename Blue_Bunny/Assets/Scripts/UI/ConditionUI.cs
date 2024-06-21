using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionUI : MonoBehaviour
{
    public HPBar HpBar;
    public MPBar MpBar;

    void Start()
    {
        UIManager.Instance.Condition = this;
    }

    void Update()
    {
        
    }
}

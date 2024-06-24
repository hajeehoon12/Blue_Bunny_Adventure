using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CallBoss : MonoBehaviour
{
   

    private void Start()
    {
             
    }

    public void CallHPBar()
    {
        transform.DOLocalMove(new Vector3(0, -175, 0), 1f);
    }

    public void GoBackHPBar()
    {
        transform.DOLocalMove(new Vector3(0, 100, 0), 1f);
    }

}

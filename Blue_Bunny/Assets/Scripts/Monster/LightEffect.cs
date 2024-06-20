using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightEffect : MonoBehaviour
{
    
    void Start()
    {
        transform.position += new Vector3(0, 0, -2);
        //Debug.Log("Hello");
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {

         
        Transform target = CharacterManager.Instance.Player.controller.pet.transform;
        //transform.LookAt(target);

        transform.DOLocalMove(target.position, 4).SetEase(Ease.OutCirc);

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }


}

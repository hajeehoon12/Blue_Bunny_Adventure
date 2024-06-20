using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightEffect : MonoBehaviour
{

    Transform target;

    void Start()
    {
        transform.position += new Vector3(0, 0, -2);
        
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        target = CharacterManager.Instance.Player.controller.pet.transform;
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {

        float interval = 0.2f;
        float distance = 10f;
        float firstDistance = Vector3.Distance(transform.position, target.position);

        while (distance > 0.2f)
        {
            distance = Vector3.Distance(transform.position, target.position);
            transform.DOLocalMove(target.position, 4 * distance/firstDistance);
            yield return new WaitForSeconds(interval);
        }
        Destroy(gameObject);
    }


}

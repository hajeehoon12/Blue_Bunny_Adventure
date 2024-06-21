using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightEffect : MonoBehaviour
{

    Transform target;
   


    private void Awake()
    {
        
    }
    void Start()
    {
        transform.position += new Vector3(0, 0, -2);
        
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        target = CharacterManager.Instance.Player.controller.pet.transform;
        StartCoroutine(Bound());
        StartCoroutine(LifeTime());
    }

    IEnumerator Bound()
    {
        float Dir = CharacterManager.Instance.Player.GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        float time = 0f;
        float timeInterval = 0.015f;

        while (time < 0.5f)
        {
            time += timeInterval;
            transform.position += new Vector3(Dir * 0.02f, 0.02f);
            yield return new WaitForSeconds(timeInterval);
        }

        
        yield return null;
        
    }

    IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(0.5f);
        
        float interval = 0.2f;
        float distance = 10f;
        float firstDistance = Vector3.Distance(transform.position, target.position);
        float time = 0f;
        float totalTime = 3f;

        while (distance > 0.2f || time < 2f)
        {
            time += 0.2f;
            float fraction = time / totalTime;

            transform.DOLocalMove(transform.position + (target.position-transform.position) * fraction, interval);
            distance = Vector3.Distance(transform.position, target.position);



            transform.DOLocalMove(target.position, 4 * distance / firstDistance);

            yield return new WaitForSeconds(interval);
        }

        


        Destroy(gameObject);
    }
}

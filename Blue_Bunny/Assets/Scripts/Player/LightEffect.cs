using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class LightEffect : MonoBehaviour
{

    //Transform CharacterManager.Instance.Player.pet.transform;


    Coroutine thisCoroutine;

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
        //CharacterManager.Instance.Player.pet.transform = CharacterManager.Instance.Player.pet.transform;
        StartCoroutine(Bound());
        thisCoroutine = StartCoroutine(LifeTime());
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
        float firstDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.pet.transform.position);
        float time = 0f;
        float totalTime = 3f;

        while (distance > 0.2f || time < 2f)
        {
            time += 0.2f;
            float fraction = time / totalTime;

            transform.DOLocalMove(transform.position + (CharacterManager.Instance.Player.pet.transform.position-transform.position) * fraction, interval);
            distance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.pet.transform.position);



            transform.DOLocalMove(CharacterManager.Instance.Player.pet.transform.position, 4 * distance / firstDistance);

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(interval);
        LifeTimeEnd();  
    }

    void LifeTimeEnd()
    {
        CharacterManager.Instance.Player.stats.AddGold(20);
        StopAllCoroutines();
        Destroy(gameObject, 0.2f);
    }


}

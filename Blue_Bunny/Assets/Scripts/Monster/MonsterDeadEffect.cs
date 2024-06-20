using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterDeadEffect : MonoBehaviour
{
    public GameObject _monsterEffect;

    private void Start()
    {
        Invoke("Dead", 2f);
    }

    public void Dead()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FadeOut());
        
    }

    IEnumerator FadeOut()
    {
        Instantiate(_monsterEffect, transform.position, Quaternion.identity);
        
        GetComponentInChildren<SpriteRenderer>().DOFade(0, 2f); // .OnComplete(() =>);
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);

    }


}

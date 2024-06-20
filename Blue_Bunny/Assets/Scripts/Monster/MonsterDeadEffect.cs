using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterDeadEffect : MonoBehaviour
{
    public GameObject _monsterEffect;
    public float tempHealth = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger!");
        if (collision.gameObject.CompareTag(Define.BULLET_TAG))
        {
            Debug.Log("Bullet Hit!!");
            tempHealth--;
            if (tempHealth <= 0)
            {
                Dead();
            }
        }
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

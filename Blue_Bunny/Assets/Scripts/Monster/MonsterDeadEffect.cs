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
        
        if (collision.gameObject.CompareTag(Define.BULLET_TAG)) // When Hit by Bullet
        {
            //if(invincible) return // when hit by bullet by near time return
            tempHealth--;
            if (tempHealth <= 0)
            {
                Dead();
            }
            else
            { 
                // monster get attacked motion; 
            }

        }
    }

    public void Dead()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FadeOut());
        GameManager.Instance.spawnManager.ApplyAliveMonsterDeath();
    }

    IEnumerator FadeOut()
    {
        Instantiate(_monsterEffect, transform.position, Quaternion.identity);
        
        GetComponentInChildren<SpriteRenderer>().DOFade(0, 2f); // .OnComplete(() =>);
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().color += Color.black;
    }


}

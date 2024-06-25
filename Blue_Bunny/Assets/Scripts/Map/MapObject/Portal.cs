using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            Debug.Log("다음 스테이지로 이동합니다");
            AudioManager.instance.PlaySFX("Warp", 0.2f);
            GameManager.Instance.ChangeMap();      
            GameManager.Instance.IsMapChanged = true;
        }
    }
}

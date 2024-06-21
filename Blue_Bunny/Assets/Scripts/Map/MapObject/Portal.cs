using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            Debug.Log("다음 스테이지로 이동합니다");
            GameManager.Instance.ChangeMap();
        }
    }
}

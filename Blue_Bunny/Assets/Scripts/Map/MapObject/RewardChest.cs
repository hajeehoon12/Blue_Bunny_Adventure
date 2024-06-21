using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    [SerializeField] SpriteRenderer chestIcon;
    [SerializeField] GameObject upArrowIcon;
    public Sprite openedChestSprite;

    public void OpenChest()
    {
        chestIcon.sprite = openedChestSprite;
        AudioManager.instance.PlaySFX("Boxopen", 0.2f);
        upArrowIcon.SetActive(false);
        GameManager.Instance.spawnManager.SpawnBoxRewardItem(this.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            upArrowIcon.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                OpenChest();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            upArrowIcon.SetActive(false);
        }
    }
}

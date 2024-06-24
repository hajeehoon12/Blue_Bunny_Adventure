using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BossItem : MonoBehaviour, iStatUpgrade
{
    public BossItemDataSO itemData;
    [SerializeField] GameObject upArrow;
    [SerializeField] TextMeshPro text;

    // 세 가지중 한 가지를 선택할 수 있는 아이템 
    // 스탯 업그레이드는 일단 존재하지 않음.
    protected virtual void SelectItem()
    {
        PlayUpgradeSound();
        GotoInventoryTab();
        UpgradeStat();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 문구 표시 ( + 화살표 윗키 표시)
        // ex) 이 아이템을 선택할까요?
        upArrow.SetActive(true);
        text.gameObject.SetActive(true);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.UpArrow) && collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            SelectItem();
            gameObject.SetActive(false);
            GameManager.Instance.spawnManager.nowMap.gameObject.GetComponent<BossMap>().RemoveUnSelectedBossItem();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        upArrow.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public abstract void UpgradeStat();

    public void GotoInventoryTab()
    {
        UIManager.Instance.Item.AddItem(itemData);
    }

    public void PlayUpgradeSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }
}

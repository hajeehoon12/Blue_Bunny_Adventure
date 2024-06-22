using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface iHeal
{
    public void Heal();
    public void PlayHealSound();
}

public interface iStatUpgrade
{ 
    public void UpgradeStat();
    public void GotoInventoryTab();
    public void PlayUpgradeSound();
}
public class Item : MonoBehaviour
{
    BoxCollider2D itemCollider;
    public ItemDataSO itemData;
    protected SpriteRenderer icon;
    private void Awake()
    {
        icon = GetComponentInChildren<SpriteRenderer>();
        itemCollider = GetComponent<BoxCollider2D>();
        itemCollider.enabled = false;
        StartCoroutine(InteractDelay());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //효과 적용
        Destroy(this.gameObject);
    }

    IEnumerator InteractDelay()
    {
        yield return new WaitForSeconds(1f);
        itemCollider.enabled = true;
    }
}


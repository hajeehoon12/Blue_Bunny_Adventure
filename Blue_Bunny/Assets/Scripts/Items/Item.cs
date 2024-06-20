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
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //효과 적용
        Destroy(this.gameObject);
    }
}


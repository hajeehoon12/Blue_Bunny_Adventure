using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface iHeal
{
    public void Heal();
}

public interface iStatUpgrade
{ 
    public void UpgradeStat();
}

public class Item : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�� �߻��߽��ϴٸ�..");   
    }
}


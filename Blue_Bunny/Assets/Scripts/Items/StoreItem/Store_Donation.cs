using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store_Donation : StoreItem
{
    [SerializeField] TextMeshPro donationText;

    protected override void SetCost()
    {
        
    }
    protected override void Buy()
    {
        if(CharacterManager.Instance.Player.stats.playerGold > 0)
        {
            Donate();
        }
    }

    public void Donate()
    {
        CharacterManager.Instance.Player.stats.playerGold -= Mathf.Min(CharacterManager.Instance.Player.stats.playerGold, itemData.cost);
        TextMeshPro donationMessage = Instantiate(donationText, this.transform);
        Destroy(donationMessage, 0.5f);
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,1f);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Buy();
        }
    }
}

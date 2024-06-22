using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store_Donation : StoreItem
{
    [SerializeField] TextMeshPro donationText;
    protected override void Buy()
    {
        Donate();
    }

    public void Donate()
    {
        Instantiate(donationText, this.transform);
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Buy();
            Destroy(gameObject, 1f);
        }
    }
}

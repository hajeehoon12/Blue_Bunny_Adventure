using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class StoreItem : MonoBehaviour
{
    public TextMeshPro costText;
    public StoreItemDataSO itemData;

    private void Awake()
    {
        SetCost();
    }
    protected virtual void SetCost()
    {
        costText.text = itemData.cost.ToString();
    }
    protected abstract void Buy();
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"가격? {itemData.cost}");
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Buy();
            Destroy(gameObject);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("안살거면 가라");
    }
}

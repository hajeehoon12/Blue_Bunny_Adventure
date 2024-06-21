using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "new ItemData")]
public class ItemDataSO : ScriptableObject
{
    [Header("ItemInfo")]
    public string itemName;
    public string itemdescription;
    public Sprite itemIcon;
}

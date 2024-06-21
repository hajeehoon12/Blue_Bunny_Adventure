using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    public void SetItemInfo(ItemDataSO itemData)
    {
        Name.text = itemData.name;
        Description.text = itemData.itemdescription;
    }
}

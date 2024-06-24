using System.Collections.Generic;

//[System.Serializable]
//public class SlotData
//{
//    //저장을 위한 직렬화된 데이터들을 모아둔 클래스

//}

[System.Serializable]
public class ItemUIData
{
    //저장을 위한 직렬화된 데이터들을 모아둔 클래스
    public List<ItemDataSO> SlotsData = new List<ItemDataSO>();

    public void LoadItem()
    {
        var itemUI = UIManager.Instance.Item;
        if(itemUI != null)
        {
            foreach (var slot in SlotsData)
            {
                //itemUI.AddItem(slot.item);
                //slot.itemIcon
            }
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class ItemUI : MonoBehaviour, IDataPersistence
{
    private bool isOn = false;      //아이템UI On, Off
    private bool isMoving = false;  //아이템UI가 움직이고 있는 중인지
    private bool IsLoaded = false;  //불러오기를 했는지

    public Slot[] slots;            //아이템 슬롯 배열
    public Transform slotPanel;     //슬롯 패널

    [Header("ItemIndex")]
    [Tooltip("아이템을 인덱스로 구분하기 위한 변수")]
    public ItemDataSO[] ItemsDataSo;

    public ItemUIData ItemsData = new ItemUIData();

    private int slotIndex;

    public ToolTip ToolTip;

    private void Start()
    {
        UIManager.Instance.Item = this;

        slots = new Slot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++) slots[i] = slotPanel.GetChild(i).GetComponent<Slot>();

        if(IsLoaded) AddItemAsLoad();
    }

    private void Update()
    {
        if(isMoving)
        {
            if(isOn)
            {
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                    new Vector2(0.0f, gameObject.transform.position.y), Time.deltaTime * 10.0f);
            }
            else
            {
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                    new Vector2(-500.0f, gameObject.transform.position.y), Time.deltaTime * 10.0f);
            }
        }

        if (gameObject.transform.position.x <= -499.99f || gameObject.transform.position.x >= -1.0f) isMoving = false;
    }

    public void OnOffUI()
    {
        if (!isMoving) isMoving = true;
        isOn = !isOn;
    }

    public void AddItem(ItemDataSO itemData)
    {
        Debug.Log($"ItemUI AddItem: {itemData.name}");

        GetEmptySlot();
        slots[slotIndex].Item = itemData;
        slots[slotIndex].Set();

        for (int i = 0; i < ItemsDataSo.Length; i++)
        {
            if (ItemsDataSo[i].name == itemData.name)
            {
                ItemsData.ItemsIndex.Add(i);
                Debug.Log($"ItemUI AddItem: {ItemsData.ItemsIndex}");
            }
        }
    }

    //업데이트UI
    void UIUpdate()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsExist) slot.Set();
            else slot.Clear();
        }
    }

    //비어있는 곳에 넣어주는 로직
    void GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsExist)
            {
                slotIndex = i;
                break;
            }
        }
    }

    public bool IsFull()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.IsExist) return false;
        }

        return true;
    }

    private void AddItemAsLoad()
    {
        foreach(int index in ItemsData.ItemsIndex)
        {
            GetEmptySlot();
            slots[slotIndex].Item = ItemsDataSo[index];
            slots[slotIndex].Set();
        }

    }

    public void LoadData(GameData data)
    {
        ItemsData.ItemsIndex = data.ItemsData;
        IsLoaded = true;
    }

    public void SaveData(GameData data)
    {
        data.ItemsData = ItemsData.ItemsIndex;
    }
}

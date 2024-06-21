using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemDataSO Item;
    public Image icon;
    public bool IsExist;
    private bool OnToolTip;

    [SerializeField] ToolTip ToolTip;

    private void Start()
    {
        icon.gameObject.SetActive(false);
        IsExist = false;
        OnToolTip = false;
    }

    private void Update()
    {
        if(OnToolTip)
        {
            ToolTip.transform.position = Input.mousePosition;
        }
    }

    //슬롯을 세팅하는 함수
    public void Set()
    {
        icon.gameObject.SetActive(true);
        IsExist = true;
        icon.sprite = Item.itemIcon;
    }

    //슬롯을 비워주는 함수
    public void Clear()
    {
        icon.gameObject.SetActive(false);
        IsExist = false;
        icon.sprite = null;
        Item = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsExist)
        {
            OnToolTip = true;
            ToolTip.gameObject.SetActive(true);
            Debug.Log(Item == null);
            ToolTip.SetItemInfo(Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsExist)
        {
            OnToolTip = false;
            ToolTip.gameObject.SetActive(false);
        }
    }
}

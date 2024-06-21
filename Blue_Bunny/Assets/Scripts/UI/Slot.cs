using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    //아이템 정보 변수
    public bool IsExist;//test
    private bool OnToolTip;

    [SerializeField] GameObject ToolTip;

    public ItemDataSO item;

    private void Start()
    {
        icon.gameObject.SetActive(false);
        IsExist = true;
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
        //아이콘 삽입

    }

    //슬롯을 비워주는 함수
    public void Clear()
    {
        icon.gameObject.SetActive(false);
        IsExist = false;
        //아이템 정보 삭제
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsExist)
        {            
            OnToolTip = true;
            ToolTip.SetActive(true);
            SetToolTipTxt();
            //툴팁 텍스트 설정
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnToolTip = false;
        ToolTip.SetActive(false);
    }

    private void SetToolTipTxt()
    {
        ToolTip.GetComponent<ToolTip>().SetItemInfo(item);
    }
}

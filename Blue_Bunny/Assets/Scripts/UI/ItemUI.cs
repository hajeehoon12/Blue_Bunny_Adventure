using UnityEngine;

public class ItemUI : MonoBehaviour
{
    private bool isOn = false;      //아이템UI On, Off
    private bool isMoving = false;  //아이템UI가 움직이고 있는 중인지

    public Slot[] slots;            //아이템 슬롯 배열
    public Transform slotPanel;     //슬롯 패널

    private void Start()
    {
        UIManager.Instance.Item = this;
        slots = new Slot[slotPanel.childCount];
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

    public void AddItem()//item 매개변수 넣어줘야함
    {
        Slot emptySlot = GetEmptySlot();
        //아이콘 추가
        //데이터 추가
    }

    //업데이트UI
    void UIUpdate()
    {
        foreach (Slot slot in slots)
        {
            if (slot != null)
            {
                //set
            }
            else
            {
                //clear
            }
        }
    }

    //비어있는 곳에 넣어주는 로직
    Slot GetEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            //슬롯이 비어 있다면 return 해준다
            if (slot.IsExist) return slot;
        }

        return null;
    }


}

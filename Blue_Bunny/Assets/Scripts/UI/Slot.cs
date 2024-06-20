using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    //������ ���� ����
    public bool IsExist;//test
    private bool OnToolTip;

    [SerializeField] GameObject ToolTip;

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

    //������ �����ϴ� �Լ�
    public void Set()
    {
        icon.gameObject.SetActive(true);
        IsExist = true;
        //������ ����

    }

    //������ ����ִ� �Լ�
    public void Clear()
    {
        icon.gameObject.SetActive(false);
        IsExist = false;
        //������ ���� ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsExist)
        {
            Debug.Log(IsExist);

            OnToolTip = true;
            ToolTip.SetActive(true);

            //���� �ؽ�Ʈ ����
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnToolTip = false;
        ToolTip.SetActive(false);
    }
}

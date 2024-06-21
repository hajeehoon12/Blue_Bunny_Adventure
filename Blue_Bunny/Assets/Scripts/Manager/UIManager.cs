using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }
    }

    public MenuUI Menu;
    public ItemUI Item;
    public ConditionUI Condition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}

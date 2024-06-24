using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_Save : MonoBehaviour
{
    [SerializeField] private Button Button;

    private void Awake()
    {
        Button.onClick.AddListener(() => DataPersistenceManager.Instance.SaveGame());
    }
}

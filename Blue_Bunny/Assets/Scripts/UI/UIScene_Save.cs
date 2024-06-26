using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인 씬에 배치되는 저장 버튼
/// </summary>
public class UIScene_Save : MonoBehaviour
{
    [SerializeField] private Button Button;

    private void Awake()
    {
        Button.onClick.AddListener(() => DataPersistenceManager.Instance.SaveGame());
    }
}

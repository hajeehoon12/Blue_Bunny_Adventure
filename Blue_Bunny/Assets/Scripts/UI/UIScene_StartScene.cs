using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// StartScene에 배치된 모든 UI들을 관리하는 클래스
/// </summary>
public class UIScene_StartScene : MonoBehaviour
{
    [SerializeField] Button GameStart_Button;
    [SerializeField] Button GameContinue_Button;
    [SerializeField] Button Credit_Button;
    [SerializeField] Button Exit_Button;
    [SerializeField] GameObject Credit;

    private void Awake()
    {
        GameStart_Button.onClick.AddListener(OnGameStart);
        GameContinue_Button.onClick.AddListener(OnGameContinue);
        Credit_Button.onClick.AddListener(() => { Credit.SetActive(true); });
        Exit_Button.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        Credit.SetActive(false);

        Credit.GetComponent<Button>().onClick.AddListener(() => { Credit.SetActive(false); });
    }

    private void OnGameStart()
    {
        DataPersistenceManager.Instance.IsNewGame = true;
        SceneManager.LoadScene(Define.MainScene);

    }

    private void OnGameContinue()
    {
        SceneManager.LoadScene(Define.MainScene);
    }
}

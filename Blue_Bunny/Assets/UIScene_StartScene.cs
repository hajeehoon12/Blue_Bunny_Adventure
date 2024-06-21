using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScene_StartScene : MonoBehaviour
{
    [SerializeField] Button GameStart_Button;
    [SerializeField] Button Credit_Button;
    [SerializeField] Button Exit_Button;
    [SerializeField] GameObject Credit;

    private void Awake()
    {
        GameStart_Button.onClick.AddListener(() => { SceneManager.LoadScene(Define.MainScene); });
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
}

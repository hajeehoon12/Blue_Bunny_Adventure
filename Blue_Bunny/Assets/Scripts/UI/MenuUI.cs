using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Slider bgm;
    public Slider sfx;

    private bool isOn = false;

    private void Start()
    {
        UIManager.Instance.Menu = this;
        gameObject.SetActive(isOn);
    }

    public void OnOffUI()
    {
        isOn = !isOn;
        gameObject.SetActive(isOn);

        if (isOn) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }

    public void ChangeBGM()
    {
        //AudioManager.instance
        float A = bgm.value;
        Debug.Log(A);
    }

    public void ChangeSFX()
    {
        float A = sfx.value;
        Debug.Log(A);
    }

    public void PressExitMenu()
    {
        Debug.Log("PressExitMenu");
    }

    public void PressExitGame()
    {
        Debug.Log("PressExitGame");
    }
}
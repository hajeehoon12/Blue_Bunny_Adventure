using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuUI : MonoBehaviour
{
    public Slider bgm;
    public Slider sfx;
    public AudioMixer mixer;

    public GameObject Blur;

    private bool isOn = false;

    private void Start()
    {
        UIManager.Instance.Menu = this;
        Blur.transform.localPosition = Vector3.zero;
        Blur.SetActive(isOn);
    }

    public void OnOffUI()
    {
        isOn = !isOn;
        Blur.SetActive(isOn);

        if (isOn) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }

    public void ChangeBGM()
    {
        SetBGMVolume(bgm.value);
    }

    public void ChangeSFX()
    {
        SetSFXVolume(sfx.value);
    }

    public void PressExitMenu()
    {
        Debug.Log("PressExitMenu");
    }

    public void PressExitGame()
    {
        Debug.Log("PressExitGame");
    }

    private void SetBGMVolume(float val)
    {
        mixer.SetFloat("BGM", Mathf.Log10(val) * 20);
    }

    private void SetSFXVolume(float val)
    {
        mixer.SetFloat("SFX", Mathf.Log10(val) * 20);
    }
}

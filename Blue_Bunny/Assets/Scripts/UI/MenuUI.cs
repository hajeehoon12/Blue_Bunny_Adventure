using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public Slider bgm;
    public Slider sfx;
    public AudioMixer mixer;

    public GameObject Blur;

    public Text BGMText;
    public Text SFXText;

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
        ChangeBGMText();
    }

    public void ChangeSFX()
    {
        SetSFXVolume(sfx.value);
        ChangeSFXText();
    }

    private void ChangeBGMText()
    {
        BGMText.text = "BGM: " + ((int)(bgm.value * 100)).ToString() + "%";
    }

    private void ChangeSFXText()
    {
        SFXText.text = "SFX: " + ((int)(sfx.value * 100)).ToString() + "%";
    }

    public void PressExitMenu()
    {
        OnOffUI();
    }

    public void PressExitGame()
    {
        //매니저 더 생길 때마다 추가해줘야함
        Time.timeScale = 1.0f;
        Destroy(GameManager.Instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(PoolManager.Instance.gameObject);
        Destroy(UIManager.Instance.gameObject);
        Destroy(CameraManager.Instance.gameObject);

        SceneManager.LoadScene(Define.StartScene);
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

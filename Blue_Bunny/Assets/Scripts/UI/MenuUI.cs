using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Slider bgm;
    public Slider sfx;

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

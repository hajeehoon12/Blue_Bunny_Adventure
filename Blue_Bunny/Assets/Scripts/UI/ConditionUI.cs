using System;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public HPBar HpBar;
    public MPBar MpBar;

    private Image image;

    public event Action<float> OnChangeBarAlpha;
    public event Action<float> OnChangeBGAlpha;

    //연한 색
    float softBar = 80.0f / 255.0f;
    float softBG = 3.0f / 255.0f;
    //진한 색
    float hardBar = 225.0f / 255.0f;
    float hardBG = 100.0f / 255.0f;
    //알파값
    float alphaBar;
    float alphaBG;

    //변하는 속도
    float changeSpeed = 3.0f;

    void Start()
    {
        UIManager.Instance.Condition = this;
        image = GetComponent<Image>();

        OnChangeBarAlpha += HpBar.ChangeBarAlpha;
        OnChangeBarAlpha += MpBar.ChangeBarAlpha;

        OnChangeBGAlpha += ChangeAlpha;
        OnChangeBGAlpha += HpBar.ChangeBGAlpha;
        OnChangeBGAlpha += MpBar.ChangeBGAlpha;

        alphaBar = hardBar;
        alphaBG = hardBG;
    }

    void Update()
    {
        if (Camera.main == null) return;
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(CharacterManager.Instance.Player.transform.position);
        if(viewportPosition.y < 0.15f)
        {
            if(alphaBar > softBar + 0.01f)
            {
                alphaBar = Mathf.Lerp(alphaBar, softBar, Time.deltaTime * changeSpeed);

                OnChangeBarAlpha?.Invoke(alphaBar);
            }
            if(alphaBG > softBG + 0.01f)
            {
                alphaBG = Mathf.Lerp(alphaBG, softBG, Time.deltaTime * changeSpeed);

                OnChangeBGAlpha?.Invoke(alphaBG);
            }
        }
        else
        {
            if (alphaBar < hardBar - 0.01f)
            {
                alphaBar = Mathf.Lerp(alphaBar, hardBar, Time.deltaTime);

                OnChangeBarAlpha?.Invoke(alphaBar);
            }
            if (alphaBG < hardBG - 0.01f)
            {
                alphaBG = Mathf.Lerp(alphaBG, hardBG, Time.deltaTime);

                OnChangeBGAlpha?.Invoke(alphaBG);
            }
        }
    }

    private void ChangeAlpha(float _alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);
    }
}

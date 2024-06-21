using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public HPBar HpBar;
    public MPBar MpBar;

    private Image image;

    //연한 색
    float softBar = 10.0f / 255.0f;
    float softBG = 3.0f / 255.0f;
    //진한 색
    float hardBar = 225.0f / 255.0f;
    float hardBG = 100.0f / 255.0f;
    //알파값
    float alphaBar;
    float alphaBG;

    //Color변수들
    Color hpBGColor;
    Color hpColor;
    Color mpBGColor;
    Color mpColor;

    //변하는 속도
    float changeSpeed = 3.0f;

    void Start()
    {
        UIManager.Instance.Condition = this;
        image = GetComponent<Image>();

        hpBGColor = HpBar.BackgroundBar.color;
        hpColor = HpBar.SlideBarImage.color;
        mpBGColor = MpBar.BackgroundBar.color;
        mpColor = MpBar.SlideBarImage.color;

        alphaBar = hardBar;
        alphaBG = hardBG;
    }

    void Update()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(CharacterManager.Instance.Player.transform.position);
        if(viewportPosition.y < 0.15f)
        {
            if(alphaBar > softBar + 0.01f)
            {
                alphaBar = Mathf.Lerp(alphaBar, softBar, Time.deltaTime * changeSpeed);

                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaBar);
                hpColor = new Color(hpColor.r, hpColor.g, hpColor.b, alphaBar);
                mpColor = new Color(mpColor.r, mpColor.g, mpColor.b, alphaBar);
            }
            if(alphaBG > softBG + 0.01f)
            {
                alphaBG = Mathf.Lerp(alphaBG, softBG, Time.deltaTime * changeSpeed);

                hpBGColor = new Color(hpBGColor.r, hpBGColor.g, hpBGColor.b, alphaBG);
                mpBGColor = new Color(mpBGColor.r, mpBGColor.g, mpBGColor.b, alphaBG);
            }
        }
        else
        {
            if (alphaBar < hardBar - 0.01f)
            {
                alphaBar = Mathf.Lerp(alphaBar, hardBar, Time.deltaTime);

                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaBar);
                hpColor = new Color(hpColor.r, hpColor.g, hpColor.b, alphaBar);
                mpColor = new Color(mpColor.r, mpColor.g, mpColor.b, alphaBar);
            }
            if (alphaBG < hardBG - 0.01f)
            {
                alphaBG = Mathf.Lerp(alphaBG, hardBG, Time.deltaTime);

                hpBGColor = new Color(hpBGColor.r, hpBGColor.g, hpBGColor.b, alphaBG);
                mpBGColor = new Color(mpBGColor.r, mpBGColor.g, mpBGColor.b, alphaBG);
            }
        }
    }
}

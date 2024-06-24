using UnityEngine;
using UnityEngine.UI;

public class MPBar : SlideBar
{
    public Image BackgroundBar;
    public Text MPText;

    void Start()
    {
        UpdateBar_Add();
        CharacterManager.Instance.Player.controller.UseMana += UpdateBar_Sub;
    }

    private void Update()
    {
        
    }

    //마나가 찰 때, 호출하는 함수
    public override void UpdateBar_Add()
    {
        Max = 50;
        Current = CharacterManager.Instance.Player.stats.playerMP;
        base.UpdateBar_Add();
        ChangeText();
    }

    //마나가 빠질 때, 호출하는 함수
    public override void UpdateBar_Sub()
    {
        Max = 50;
        Current = CharacterManager.Instance.Player.stats.playerMP;
        base.UpdateBar_Sub();
        ChangeText();
    }

    private void ChangeText()
    {
        MPText.text = $"{(int)Current} / {(int)Max}";
    }

    public void ChangeBGAlpha(float _alpha)
    {
        BackgroundBar.color = new Color(BackgroundBar.color.r, BackgroundBar.color.g, BackgroundBar.color.b, _alpha);
    }
}

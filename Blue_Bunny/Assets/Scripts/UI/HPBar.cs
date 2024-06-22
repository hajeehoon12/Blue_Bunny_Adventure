using UnityEngine;
using UnityEngine.UI;

public class HPBar : SlideBar
{
    public Image BackgroundBar;
    public Text HPText;

    void Start()
    {
        UpdateBar_Add();
        CharacterManager.Instance.Player.battle.OnDamage += UpdateBar_Sub;
        CharacterManager.Instance.Player.battle.OnHeal += UpdateBar_Add;
    }

    //체력이 찰 때, 호출하는 함수
    public override void UpdateBar_Add()
    {
        //가끔 초기화를 위해서 이 함수를 호출했을 때, 현재 체력이 0으로 초기화되는 버그가 있음
        //Max = CharacterManager.Instance.Player.battle.MaxHealth;
        //Current = CharacterManager.Instance.Player.battle.CurrentHealth;
        Max = CharacterManager.Instance.Player.stats.playerMaxHP;
        Current = CharacterManager.Instance.Player.stats.playerHP;
        base.UpdateBar_Add();
        ChangeText();
    }

    //체력이 빠질 때, 호출하는 함수
    public override void UpdateBar_Sub()
    {
        Max = CharacterManager.Instance.Player.battle.MaxHealth;
        Current = CharacterManager.Instance.Player.battle.CurrentHealth;
        base.UpdateBar_Sub();
        ChangeText();
    }
    private void ChangeText()
    {
        HPText.text = $"{(int)Current} / {(int)Max}";
    }

    public void ChangeBGAlpha(float _alpha)
    {
        BackgroundBar.color = new Color(BackgroundBar.color.r, BackgroundBar.color.g, BackgroundBar.color.b, _alpha);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HPBar : SlideBar
{
    public Image BackgroundBar;

    void Start()
    {
        SlideBarImage.fillAmount = 1.0f;
        UpdateBar_Add();
        CharacterManager.Instance.Player.battle.OnDamage += UpdateBar_Sub;
        CharacterManager.Instance.Player.battle.OnHeal += UpdateBar_Add;
    }

    //체력이 찰 때, 호출하는 함수
    public override void UpdateBar_Add()
    {
        Max = CharacterManager.Instance.Player.battle.MaxHealth;
        Current = CharacterManager.Instance.Player.battle.CurrentHealth;
        base.UpdateBar_Add();
    }

    //체력이 빠질 때, 호출하는 함수
    public override void UpdateBar_Sub()
    {
        Max = CharacterManager.Instance.Player.battle.MaxHealth;
        Current = CharacterManager.Instance.Player.battle.CurrentHealth;
        base.UpdateBar_Sub();
    }

    public void ChangeBGAlpha(float _alpha)
    {
        BackgroundBar.color = new Color(BackgroundBar.color.r, BackgroundBar.color.g, BackgroundBar.color.b, _alpha);
    }
}

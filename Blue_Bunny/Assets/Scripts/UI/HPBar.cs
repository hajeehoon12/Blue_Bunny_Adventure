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
    //public override void UpdateBar_Add(float amount)
    //{
    //    base.UpdateBar_Add(amount);
    //}

    ////체력이 빠질 때, 호출하는 함수
    //public override void UpdateBar_Sub(float amount)
    //{
    //    base.UpdateBar_Sub(amount);
    //}
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
}

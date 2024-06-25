using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPManager : MonoBehaviour
{
    public static BossHPManager instance;
    public Text bossText;

    [SerializeField] private Slider hpBarSlider = null;

    public CallBoss callBoss;

    private bool isCloudStage = false;
    private bool isRedStage = false;


    private void Awake()
    {
        instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        SetHPBar();
    }

    public void SyncCloud()
    {
        hpBarSlider.value = 0f;
        bossText.DOText("", 0f);
        isCloudStage = true;
        callBoss.CallHPBar();
        StartCoroutine(CloudSync());

    }

    public void SyncRed()
    {
        hpBarSlider.value = 0f;
        bossText.DOText("", 0f);
        isRedStage = true;
        callBoss.CallHPBar();
        StartCoroutine(RedSync());
    }

    IEnumerator RedSync()
    {
        yield return new WaitForSeconds(1f);

        DOTween.To(() => hpBarSlider.value, x => hpBarSlider.value = x, 1f, 1f);

        bossText.DOText("Git터져서 화난 검사", 1f);

    }


    IEnumerator CloudSync()
    {
        
        yield return new WaitForSeconds(1f);
        
        DOTween.To(() => hpBarSlider.value, x => hpBarSlider.value = x, 1f, 1f);
        
        bossText.DOText("성난 번개 구름", 1f);
        //hpBarSlider.value

    }

    public void HPBarUp()
    {
        callBoss.GoBackHPBar();
        isCloudStage = false;
        isRedStage = false;
    }

    public void SetHPBar()
    {
        if (isCloudStage)
        {
            hpBarSlider.value = CloudBoss.Instance.bossCurrentHP / CloudBoss.Instance.bossMaxHP;
        }
        if (isRedStage)
        { 
            hpBarSlider.value = BossController.Instance.bossHP / BossController.Instance.bossMaxHP;
        }

    }

}

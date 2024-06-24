using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossItemData", menuName = "new BossItemData")]
public class BossItemDataSO : ItemDataSO
{
    // 보스 관련 정보 ex)[~의] , [~을 처치한]
    public string bossIndicator;
}

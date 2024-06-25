using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGold : MonoBehaviour
{
    public Text goldText;

    private void Update()
    {
        goldText.text = CharacterManager.Instance.Player.stats.playerGold.ToString("N0");
    }

}

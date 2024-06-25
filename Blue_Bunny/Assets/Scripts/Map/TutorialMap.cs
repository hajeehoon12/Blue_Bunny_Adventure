using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMap : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPos;
    private void Start()
    {
        CharacterManager.Instance.Player.transform.position = playerSpawnPos.position;
    }
}

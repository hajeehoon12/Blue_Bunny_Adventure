using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreItemText : MonoBehaviour
{
    TextMeshPro costText;

    public float moveMax;
    public float speed;

    Vector2 pos;

    private void Awake()
    {
        costText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        Vector2 dirPos = pos;
        dirPos.y = pos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}

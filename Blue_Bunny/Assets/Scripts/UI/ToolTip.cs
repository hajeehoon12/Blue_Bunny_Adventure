using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    public void ChangeName(string name)
    {
        Name.text = name;
    }

    public void ChangeDescription(string description)
    {
        Description.text = description;
    }
}

using TMPro;
using UnityEngine;

public class TaskDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMesh;

    public void UpdateText(string value)
    {
        _textMesh.text = value;
    }
}
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialog : ScriptableObject
{
    public string ID;
    [Header("")]
    [Tooltip("Character speaking")] public Characters Character;
    [TextArea(10,5)] public string Dialogue;
    public AudioClip Clip;
    [Tooltip("Next dialogue")] public Dialog NextDialogue;

    public void UpdateDialog(TextMeshProUGUI textComponentName, TextMeshProUGUI textComponent)
    {
        textComponentName.text = Character.Name;
        textComponent.text = Dialogue;
    }
}

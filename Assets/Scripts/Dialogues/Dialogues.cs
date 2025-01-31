using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogues : ScriptableObject
{
    [Tooltip("Character speaking")] public Characters Character;
    [TextArea()] public string Dialogue;
    [Tooltip("Next dialogue")] public Dialogues NextDialogue;

    public void UpdateDialogue(TextMeshProUGUI textComponentName, TextMeshProUGUI textComponent)
    {
        textComponentName.text = Character.Name;
        textComponent.text = Dialogue;
    }
}

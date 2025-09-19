using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogText;
    public AudioSource Audio;

    [SerializeField] TaskDialog[] _dialogtasks;
    [SerializeField] GameObject _DialogParent;
    [SerializeField] Dialog _actualDialogue;
    [SerializeField] float _timerValue;

    Dialog _dialogue;
    float _timer;

    private void Start()
    {
        _DialogParent.SetActive(false);
        //_actualDialogue.UpdateDialogue(NameText, DialogText);
        _dialogue = _actualDialogue;
    }

    private void Update()
    {
        if (_actualDialogue != null)
        {
            if (_timer < _timerValue)
                _timer += Time.deltaTime;

            if (_timer >= _timerValue/* && Input.GetMouseButtonDown(0)*/)      //Input next line
            {
                NextDialogue();
            }
        }

        //if (Input.GetMouseButtonDown(1))     //Input exemple call dialog
        //{
        //    UpdateDialogue(_dialogue);
        //}
    }

    //Next line
    void NextDialogue()
    {
        if (_actualDialogue.NextDialogue != null)
        {
            _actualDialogue = _actualDialogue.NextDialogue;
            _actualDialogue.UpdateDialog(NameText, DialogText);
        }
        else
        {
            if (_dialogtasks.Length > 0)
            {
                for (int i = 0; i < _dialogtasks.Length; i++)
                {
                    if (_actualDialogue.ID == _dialogtasks[i].ID)
                        _dialogtasks[i].TaskComplete();
                }
            }

            _DialogParent.SetActive(false);
            _actualDialogue = null;
        }

        _timer = 0;
    }

    //Activate dialog box
    public void UpdateDialogue(Dialog dialogue)
    {
        if (dialogue != null)
        {
            _DialogParent.SetActive(true);
            _actualDialogue = dialogue;
            _actualDialogue.UpdateDialog(NameText, DialogText);

            if (_actualDialogue.Clip != null)
            {
                Audio.clip = _actualDialogue.Clip;
                Audio.Play();
            }

            _timer = 0;
        }
    }
}

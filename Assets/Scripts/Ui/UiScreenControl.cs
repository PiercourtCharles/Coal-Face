using UnityEngine;
using UnityEngine.UI;

public class UiScreenControl : MonoBehaviour
{
    [SerializeField] Image _air;
    [SerializeField] Image[] _fuses;
    [SerializeField] float _airLoss = 5;

    private void Start()
    {
        _air.fillAmount = 1;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        //Air
        //if (GameManager.Instance.PanelControl != null && !GameManager.Instance.PanelControl.Power.IsActive ||
        //    GameManager.Instance.Fuses.Length >= 1 && GameManager.Instance.Fuses[1] != null && GameManager.Instance.Fuses[1].IsBreak)
        //{
        //    _air.fillAmount -= _airLoss * Time.deltaTime;

        //    if (_air.fillAmount <= 0)
        //    {
        //        _air.fillAmount = 0;
        //        PlayerComponentManager.Instance.Stats.Air -= Time.deltaTime;
        //        PlayerComponentManager.Instance.Stats.CheckDeath();
        //    }
        //}
        //else
        //{
        //    _air.fillAmount += _airLoss * Time.deltaTime;

        //    if (_air.fillAmount >= 1)
        //        _air.fillAmount = 1;
        //}
    }

    private void FixedUpdate()
    {
        //Fuses
        for (int i = 0; i < _fuses.Length; i++)
        {
            if (GameManager.Instance == null)
                return;

            if (GameManager.Instance.Fuses.Length == _fuses.Length)
            {
                if (GameManager.Instance.Fuses[i].IsBreak)
                    _fuses[i].color = Color.red;
            }
            else
                _fuses[i].color = Color.green;
        }
    }
}

using UnityEngine;

public class VehiculePanelManager : MonoBehaviour
{
    public Interactible Power;
    public Interactible PowerLights;
    public Interactible Drill;

    [SerializeField] PowerLever _powerLever;
    [SerializeField] Crank _crank;
    [SerializeField] float _speedMove;
    [SerializeField] float _speedRot;

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        float movement = 0;
        float rotation = 0;

        if (GameManager.Instance.Furnase != null && GameManager.Instance.Furnase.IsBreak)
            Power.ChangeTarget(false);

        if (Power.IsActive)
        {
            //if (GameManager.Instance.Shaker != null)
            //StartCoroutine(GameManager.Instance.Shaker.Shake());

            if (_powerLever.Value != 0)
                movement += Mathf.Sign(_powerLever.Value);

            if (_crank.Angle != 0)
                rotation += Mathf.Sign(_crank.Angle);
        }

        if (movement != 0 || rotation != 0)
        {
            movement = movement * _speedMove * Time.deltaTime;
            rotation = rotation * _speedRot * Time.deltaTime;

            GameManager.Instance.Mining.Move(new Vector3(0, movement), Quaternion.Euler(new Vector3(0, 0, rotation)));
        }
    }
}
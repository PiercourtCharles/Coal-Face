using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject DirtPrefab;
    public GameObject RockPrefab;
    public GameObject CoalPrefab;

    [SerializeField] Transform _parentRessources;
    [SerializeField] Portal _spawnPointRessources;
    [Header("Move")]
    [SerializeField] float _speed;

    float _actualSpeed;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.Furnase != null && GameManager.Instance.Furnase.IsBreak && _actualSpeed > 0)
        {
            _actualSpeed -= Time.deltaTime;

            if (_actualSpeed < 0)
                _actualSpeed = 0;
        }
        else if (_actualSpeed < _speed && GameManager.Instance.PanelControl != null && GameManager.Instance.PanelControl.Power.IsActive)
        {
            _actualSpeed += Time.deltaTime;

            if (_actualSpeed > _speed)
                _actualSpeed = _speed;
        }
    }

    public void SpawnRessource(GameObject obj)
    {
        var objInst = Instantiate(obj, _spawnPointRessources.Gate.position, Quaternion.identity, _parentRessources);
        objInst.transform.localScale *= Random.Range(1, 2);
    }
}

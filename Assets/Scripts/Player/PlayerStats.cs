using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Air = 3;
    public float Life = 0;
    public bool IsDead = false;

    [SerializeField] float LifeMax = 10;

    private void Start()
    {
        Life = LifeMax;
    }

    public void CheckDeath()
    {
        if (Air <= 0)
            Life -= Time.deltaTime;

        if (Life <= 0)
        {
            GameManager.Instance.Ui.UiPlayer.Dead();
            IsDead = true;
        }
    }

    public float GetLife()
    {
        return Life / LifeMax;
    }
}

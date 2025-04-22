using System.Collections;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public BedInteraction PanelInt = new BedInteraction();
    public SpriteRenderer _render;
    public Transform CamPosPanel;
    public bool IsInBed = false;
    Coroutine _coroutine = null;

    private void Start()
    {
        _render.color = new Color(_render.color.r, _render.color.g, _render.color.b, 0);
    }

    public void ChangePanelMod()
    {
        IsInBed = !IsInBed;
        Debug.Log("In bed " + IsInBed);
        var player = PlayerManager.Instance;

        if (IsInBed)
        {
            //player.Movement.IsParalysed = true;
            //player.Look.IsOnHead = false;
            //player.Look.CamTargetPos = CamPosPanel;
            _coroutine = StartCoroutine(FadePanel(true));
        }
        else
        {
            //player.Movement.IsParalysed = false;
            //player.Look.IsOnHead = true;
            //player.Look.CamTargetPos = null;
            _coroutine = StartCoroutine(FadePanel(false));
        }
    }

    public IEnumerator FadePanel(bool valid)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        var player = PlayerManager.Instance;

        if (valid)
        {
            player.Movement.IsParalysed = true;
            player.Look.IsOnHead = false;
            player.Look.CamTargetPos = CamPosPanel;

            for (float i = 0; _render.color.a < 1; i += Time.deltaTime)
            {
                _render.color = new Color(_render.color.r, _render.color.g, _render.color.b, i);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _coroutine = null;
        }
        else
        {
            for (float i = 1; _render.color.a > 0; i -= Time.deltaTime)
            {
                _render.color = new Color(_render.color.r, _render.color.g, _render.color.b, i);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            player.Movement.IsParalysed = false;
            player.Look.IsOnHead = true;
            player.Look.CamTargetPos = null;
            _coroutine = null;
        }
    }
}

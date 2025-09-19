using System.Collections;
using UnityEngine;

public class TaskWait : Task
{
    [SerializeField] float _waitingTime;

    Coroutine _coroutine;

    public override void Begin()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Waiting());
    }

    public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_waitingTime);
        TaskComplete();
    }
}

using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float TimeDuration = 1;
    public float Magnitude = 0.01f;
    public float Speed = 0.01f;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartCoroutine(Shake(_timeDuration, _magnitudeLoss));
    //    }
    //}

    public IEnumerator Shake()
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0;

        while (elapsed < TimeDuration)
        {
            float x = Random.Range(-1f, 1f) * Magnitude;
            float y = Random.Range(-1f, 1f) * Magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}

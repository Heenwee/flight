using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalRot = transform.localEulerAngles;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            if (Time.timeScale != 0)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                float z = Random.Range(-1f, 1f) * magnitude;

                transform.localEulerAngles = new Vector3(originalRot.x + x, originalRot.y + y, originalRot.z + z);
                elapsed += Time.deltaTime / Time.timeScale;
            }
            else elapsed = duration;

            yield return null;
        }

        transform.localEulerAngles = new Vector3(0,0,0);
    }
}

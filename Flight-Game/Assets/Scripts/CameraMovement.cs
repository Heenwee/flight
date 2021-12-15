using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Transform camPos;
    Vector3 posOffset;

    public float moveCam;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = camPos.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        CamPos();
    }
    void Follow()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
    void CamPos()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = -Input.GetAxisRaw("Vertical");
        Vector3 pos = new Vector3(x, y, 0).normalized * moveCam;

        camPos.localPosition = Vector3.Lerp(camPos.localPosition, pos + posOffset, lerpSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollision : MonoBehaviour
{
    LayerMask layerMask;
    Vector3 prevPos;

    float betw;

    private void Awake()
    {
        ////////GETS THE LAYERS OBJECT CAN COLLIDE WITH THROUGH COLLISION MAYTIX/////////
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(gameObject.layer, i))
            {
                layerMask |= 1 << i;
            }
        }
        prevPos = transform.position;
        betw = Vector3.Distance(transform.position, prevPos);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 lookAt = prevPos;
        //transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((lookAt.y - transform.position.y), (lookAt.x - transform.position.x)) * Mathf.Rad2Deg);

        betw = Vector3.Distance(transform.position, prevPos);

        Debug.DrawLine(prevPos, transform.position + transform.forward * betw, Color.red);

        if (Physics.Raycast(prevPos, transform.forward, out RaycastHit hit, betw, layerMask))
        {
            if (hit.transform != null)
            {
                transform.position = hit.point;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        prevPos = transform.position;
    }
}

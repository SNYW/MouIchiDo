using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minx;
    public float maxx;

    public bool cameraFollow;

    private void Start()
    {
        cameraFollow = false;
    }

    private void Update()
    {
        if (cameraFollow)
        {
            var camVect =
            new Vector3(
                Mathf.Clamp(Samurai.instance.transform.position.x, minx, maxx),
                transform.position.y,
                transform.position.z);

            transform.position = camVect;
        }
    }
}

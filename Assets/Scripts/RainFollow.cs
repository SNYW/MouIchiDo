using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollow : MonoBehaviour
{
    public static RainFollow instance;

    public bool follow;
    public float offset;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            var rainVect =
            new Vector3(Samurai.instance.transform.position.x+offset,
                transform.position.y,
                transform.position.z);

            transform.position = rainVect;
        }
    }
}

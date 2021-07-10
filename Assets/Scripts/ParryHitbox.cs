using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Samurai.instance.Parry();
    }
}

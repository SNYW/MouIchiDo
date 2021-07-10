using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public HitboxManager parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            parent.LightHit(collision.gameObject);
        }
        else
        {
            parent.LightHit(collision.gameObject);
        }
    }
}

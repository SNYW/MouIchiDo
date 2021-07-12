using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 11)
        {
            transform.parent.GetComponent<EnemyHitboxManager>().LightHit(collision.gameObject);
        }
    }
}

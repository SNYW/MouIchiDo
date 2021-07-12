using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxManager :HitboxManager
{
    public Enemy parentEnemy;

    public override void LightHit(GameObject g)
    {
        Debug.Log("hit");
    }
}

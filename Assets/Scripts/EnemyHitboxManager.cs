using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxManager :HitboxManager
{
    public Enemy parentEnemy;

    public override void LightHit(GameObject g)
    {
        if (!Samurai.instance.parrying)
        {
            Samurai.instance.currentHits-=parentEnemy.attackDamage;
            Samurai.instance.SwordHitSound();
            var splat = Instantiate(bloodSplat, Samurai.instance.bleedanchor.position, Quaternion.identity);
            Destroy(splat, 1);
        }

    }
}

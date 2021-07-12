using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Samurai.instance.Parry(collision.transform.parent.GetComponent<EnemyHitboxManager>().parentEnemy);
        Samurai.instance.ParrySound();
    }
}

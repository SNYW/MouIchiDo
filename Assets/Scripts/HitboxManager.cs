using UnityEngine;

public class HitboxManager : MonoBehaviour
{
   public GameObject bloodSplat;
   public virtual void LightHit(GameObject hitobject)
    {
       Enemy e = hitobject.GetComponent<Enemy>();
       if (e != null && !e.dead)
       {
            var splat = Instantiate(bloodSplat, e.bleedPosition.position, Quaternion.identity);
            Samurai.instance.SwordHitSound();
            Destroy(splat, 1);
            e.OnHit();
       }
    }
}

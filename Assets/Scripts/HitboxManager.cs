using UnityEngine;

public class HitboxManager : MonoBehaviour
{
   public GameObject bloodSplat;
   public virtual void LightHit(GameObject hitobject)
    {
       Enemy e = hitobject.GetComponent<Enemy>();
       if (e != null)
       {
            var splat = Instantiate(bloodSplat, e.bleedPosition.position, Quaternion.identity);
            Destroy(splat, 1);
            e.OnHit();
       }
    }
}

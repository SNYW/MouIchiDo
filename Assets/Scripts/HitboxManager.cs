using UnityEngine;

public class HitboxManager : MonoBehaviour
{
   public GameObject bloodSplat;
   public void LightHit(Vector3 hitpos)
    {
       var splat =  Instantiate(bloodSplat, hitpos, Quaternion.identity);
       Destroy(splat, 1);
    }
}

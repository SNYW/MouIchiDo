using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLaunch : MonoBehaviour
{
    private SpriteRenderer sprite;
    public ParticleSystem bleeding;
    public float fadeOutSpeed;
    public float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.material = Instantiate(sprite.material);
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.material.SetFloat("visibility",
        sprite.material.GetFloat("visibility") - Time.deltaTime * fadeOutSpeed);

        if (sprite.material.GetFloat("visibility") <= 0)
        {
            Destroy(gameObject);
        }
        if (sprite.material.GetFloat("visibility") < 0.5f)
        {
            bleeding.Stop();
        }
    }

    private void Launch()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(-45, 45));
        var grb = GetComponent<Rigidbody2D>();
        grb.AddForce(transform.forward + Vector3.up* Random.Range(launchForce-2, launchForce+2)+Vector3.left, ForceMode2D.Impulse);
        grb.AddTorque(Random.Range(2f, 4f), ForceMode2D.Impulse);
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform bleedPosition;
    public GameObject head;
    private Animator animator;
    public SpriteRenderer sprite;
    public float fadeOutSpeed;

    public bool dead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dead = false;
    }

    private void Update()
    {
        ManageDeath();
    }

    public void OnHit()
    {
        Die();
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            animator.Play("Death");
            Instantiate(head, bleedPosition.position, Quaternion.identity);
        }
    }

    private void ManageDeath()
    {
        if (dead)
        {
            sprite.color =
                new Color(
                sprite.color.r,
                sprite.color.g,
                sprite.color.b,
                sprite.color.a - Time.deltaTime * fadeOutSpeed);

            if (sprite.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

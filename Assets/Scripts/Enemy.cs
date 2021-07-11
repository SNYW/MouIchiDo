using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hits;
    public int maxHits;

    public Transform bleedPosition;
    public GameObject head;
    private Animator animator;
    public SpriteRenderer sprite;
    public float fadeOutSpeed;
    public float attackRange;
    public float moveSpeed;

    public bool dead;
    public bool canMove;

    private void Start()
    {
        canMove = true;
        hits = Random.Range(2, maxHits);
        animator = GetComponent<Animator>();
        dead = false;
    }

    private void Update()
    {
        ManageDeath();
        if (canMove && !dead)
        {
            MoveDecision();
        }
    }

    public void OnHit()
    {
        hits--;
        if(hits <= 0)
        {
            Die();
        }
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

    public void Parried()
    {
        animator.StopPlayback();
        canMove = false;
        animator.Play("Parried");
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

    private void MoveDecision()
    {
        if (Samurai.instance.transform.position.x < transform.position.x - attackRange)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
            var clip = animator.GetCurrentAnimatorClipInfo(0);
            if (clip[0].clip.name != "Parried")
            {
                animator.Play("Attack");
            }
        }
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hits;
    public int maxHits;
    public int attackDamage;

    public Transform bleedPosition;
    public GameObject head;
    public Animator animator;
    public SpriteRenderer sprite;
    public float fadeOutSpeed;
    public float attackRange;
    public float moveSpeed;

    public bool dead;
    public bool canMove;
    public bool active;

    private void Start()
    {
        sprite.material = Instantiate(sprite.material);
        active = false;
        canMove = true;
        hits = Random.Range(3, maxHits);
        animator = GetComponent<Animator>();
        dead = false;
    }

    private void Update()
    {
        if (active && Samurai.instance.alive)
        {
            ManageDeath();
            if (canMove && !dead)
            {
                MoveDecision();
            }
        }
       
    }

    public void OnHit()
    {
        hits--;
        active = true;
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
            active = true;
            animator.Play("Death");
            Samurai.instance.SwordHitSound();
            AudioSource a = GetComponent<AudioSource>();
            a.pitch = Random.Range(0.8f, 1.2f);
            a.Play();
            var h = Instantiate(head, bleedPosition.position, Quaternion.identity);
            h.transform.localScale = transform.localScale*6;
            if (GameManager.gm.enemies.Contains(this))
            {
                GameManager.gm.enemies.Remove(this);
                GameManager.gm.ActivateNextEnemy();
            }
        }
    }

    public void Parried()
    {
        animator.StopPlayback();
        canMove = false;
        animator.Play("Parried");
        Samurai.instance.currentHits =
            Mathf.Clamp(Samurai.instance.currentHits+1, 0, Samurai.instance.maxHits);
    }

    private void ManageDeath()
    {
        if (dead)
        {
            sprite.material.SetFloat("visibility",
            sprite.material.GetFloat("visibility") - Time.deltaTime * fadeOutSpeed);

            if (sprite.material.GetFloat("visibility") < 0)
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

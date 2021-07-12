using UnityEngine;
using UnityEngine.Playables;

public class Samurai : MonoBehaviour
{
    public static Samurai instance;
    public int maxHits;
    public int currentHits;
    public Animator animator;

    public int comboIndex;
    public int comboMax;
    public bool inCombo;

    public bool canWalk;
    public float moveSpeed;

    public Collider2D[] hitBoxes;
    public PlayableDirector timeline;

    public bool alive;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        comboIndex = 1;
        animator = GetComponent<Animator>();
        timeline = GetComponent<PlayableDirector>();
        ResetSamurai();
    }
    
    void Update()
    {
        if (alive)
        {
            alive = currentHits > 0;
            if (!alive)
            {
                Die();
            }

            animator.SetBool("walking", false);

            if (Input.GetKeyDown(KeyCode.Mouse0) && comboIndex == 1)
            {
                if (CanCombo())
                {
                    animator.Play("Attack" + comboIndex);
                    comboIndex++;
                    canWalk = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.Play("Parry");
                canWalk = false;
                comboIndex = 1;
            }

            else
            {
                if (canWalk)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("walking", true);
                        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("walking", true);
                        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                    }
                }
            }
        }
        
       
    }

    private void ResetSamurai()
    {
        currentHits = maxHits;
    }

    public void ResetCombo()
    {
        comboIndex = 1;
    }

    public bool CanCombo()
    {
        return comboIndex <= comboMax;
    }

    public void Combo(string s)
    {
                canWalk = false;
        animator.Play(s + comboIndex);
        comboIndex++;
    }

    public void Parry(Enemy target)
    {
        foreach (Collider2D c in hitBoxes)
        {
            c.gameObject.SetActive(false);
        }
        canWalk = true;
        target.Parried();
    }

    public void Die()
    {
        timeline.Play();
    }
}

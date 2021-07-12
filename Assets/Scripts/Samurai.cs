using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Samurai : MonoBehaviour
{
    public static Samurai instance;
    public int maxHits;
    public int currentHits;
    public Transform bleedanchor;
    public bool parrying;
    public Animator animator;

    public int comboIndex;
    public int comboMax;
    public bool inCombo;

    public bool canWalk;
    public float moveSpeed;

    public PlayableDirector timeline;

    public bool alive;
    public Image[] lives;
    public GameObject livesPanel;
    public Sprite lifeFull;
    public Sprite lifeEmpty;

    private void Awake()
    {
        instance = this; 
    }
    
    void Start()
    {
        comboIndex = 1;
        animator = GetComponent<Animator>();
        timeline = GetComponent<PlayableDirector>();
        timeline.Pause();
        livesPanel.SetActive(alive);
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
            ManageLives();
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
                    else if (Input.GetKey(KeyCode.D) && transform.position.x < GameManager.gm.activeEnemy.transform.position.x-1.3f)
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
        canWalk = true;
        target.Parried();
    }

    public void Die()
    {
        timeline.Play();
    }

    public void ManageLives()
    {
        livesPanel.SetActive(alive);

        for (int i = 0; i < lives.Length; i++)
        {
            if(i <= currentHits - 1)
            {
                lives[i].sprite = lifeFull;
            }
            else
            {
                lives[i].sprite = lifeEmpty;
            }
        }
    }
}

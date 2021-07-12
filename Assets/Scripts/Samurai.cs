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

    public AudioClip swordHit;
    public AudioClip swordSwing;
    public AudioClip parry;
    public AudioSource soundPlayer;
    public AudioSource parrySoundPlayer;

    private void Awake()
    {
        instance = this; 
    }
    
    void Start()
    {
        comboIndex = 1;
        animator = GetComponent<Animator>();
        timeline = GetComponent<PlayableDirector>();
        soundPlayer = GetComponent<AudioSource>();
        timeline.Pause();
        livesPanel.SetActive(alive);
        ResetSamurai();
    }
    
    void Update()
    {
        if (alive)
        {
            alive = currentHits > 0;
            ManageLives();
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
                    SwordSwingSound();
                    comboIndex++;
                    canWalk = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.Play("Parry");
                SwordSwingSound();
                canWalk = false;
                comboIndex = 1;
            }

            else
            {
                if (canWalk)
                {
                    if (Input.GetKey(KeyCode.A) && transform.position.x > -9)
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
        SwordSwingSound();
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

    public void SwordSwingSound()
    {
        soundPlayer.pitch = Random.Range(0.8f, 1.2f);
        soundPlayer.PlayOneShot(swordSwing);
    }
    public void SwordHitSound()
    {
        soundPlayer.pitch = Random.Range(0.8f, 1.2f);
        soundPlayer.PlayOneShot(swordHit);
    }
    public void ParrySound()
    {
        parrySoundPlayer.pitch = Random.Range(0.8f, 1.2f);
        parrySoundPlayer.PlayOneShot(parry);
    }
}

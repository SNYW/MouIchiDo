using UnityEngine;

public class Samurai : MonoBehaviour
{
    public static Samurai instance;
    public int maxHits;
    public int currentHits;
    private Animator animator;

    public int comboIndex;
    public int comboMax;
    public bool inCombo;

    public bool canWalk;
    public float moveSpeed;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        comboIndex = 1;
        animator = GetComponent<Animator>();
        ResetSamurai();
    }
    
    void Update()
    {

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
        else if (Input.GetKeyDown(KeyCode.Mouse1) && canWalk)
        {
            animator.Play("Parry");
            canWalk = false;
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
}

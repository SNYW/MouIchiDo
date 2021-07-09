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
        if (Input.GetKeyDown(KeyCode.Mouse0) && comboIndex == 1)
        {
            if(CanCombo())
            {
                animator.Play("Attack" + comboIndex);
                comboIndex++;
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
        animator.Play(s + comboIndex);
        comboIndex++;
    }
}

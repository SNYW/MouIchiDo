using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOni : MonoBehaviour
{

    public List<Enemy> cutsceneEnemies;
    public float killDelay;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneEnemies = new List<Enemy>();
        foreach(Enemy e in GetComponentsInChildren<Enemy>())
        {
            cutsceneEnemies.Add(e);
        }

        foreach(Enemy e in cutsceneEnemies)
        {
            e.GetComponent<Animator>().Play("CutsceneHold");
        }
    }

    public void KillCutsceneEnemies()
    {
        InvokeRepeating("KillNextEnemy", 0, killDelay);
    }

    private void KillNextEnemy()
    {
        if(cutsceneEnemies.Count > 0)
        {
            cutsceneEnemies[0].Die();
            cutsceneEnemies.RemoveAt(0);
        }
    }
}

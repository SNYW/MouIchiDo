using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOni : MonoBehaviour
{

    private List<Enemy> cutsceneEnemies;
    public float killDelay;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Enemy e in GetComponentsInChildren<Enemy>())
        {
            cutsceneEnemies.Add(e);
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

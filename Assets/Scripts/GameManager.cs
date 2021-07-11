using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int startEnemyAmount;
    public float spawnstartx;
    public float spawnOffset;
    public GameObject[] enemyTypes;
    public List<Enemy> enemies;
    public Enemy activeEnemy;

    public bool cutscenepause1;
    
    private void Awake()
    {
        cutscenepause1 = false;
        gm = this;
    }

    private void Update()
    {
        if (cutscenepause1)
        {
            Samurai.instance.animator.Play("Cutscene1");
        }
    }

    public void StartGame()
    {
        float currX = spawnstartx;
        for (int i = 0; i < startEnemyAmount; i++)
        {
            var enemy = Instantiate(
                enemyTypes[Random.Range(0, enemyTypes.Length - 1)],
                new Vector3(currX + spawnOffset, -4.51f, 0),
                Quaternion.identity).GetComponent<Enemy>();
            currX = enemy.transform.position.x;
            enemies.Add(enemy);
        }

        enemies[0].active = true;
        activeEnemy = enemies[0];

    }

    public void ActivateNextEnemy()
    {
        enemies[0].active = true;
        activeEnemy = enemies[0];
    }

    public void PlayOpeningCutscene()
    {
        GetComponent<PlayableDirector>().Play();
        cutscenepause1 = false;
    }

    public void PauseForMenu()
    {
        cutscenepause1 = true;
    }

}

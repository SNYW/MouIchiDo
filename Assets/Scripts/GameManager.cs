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
    public PlayableDirector timeline;
    public PlayableDirector endGameTimeline;

    //UI
    public GameObject[] uiToDisable;

    public bool clickToResume;
    public bool gameStart;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        gm = this;
        gameStart = false;
    }

    private void Start()
    {
        CutscenePositions();
    }

    private void Update()
    {
        if (clickToResume)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clickToResume = false;
                UnpauseTimeline();
            }
        }

        if(activeEnemy != null)
        {
            activeEnemy.active = true;
        }
        else
        {
            if (gameStart)
            {
                activeEnemy = enemies[0];
            }
        }
    }

    public void SkipCutscene()
    {
        timeline.Stop();
        foreach (GameObject g in uiToDisable)
        {
            g.SetActive(false);
        }
        ClearEnemies();
        SetupGamePositions();
        StartGame();
    }

    public void ClearEnemies()
    {
        if(activeEnemy != null)
        {
            enemies.Remove(activeEnemy);
            Destroy(activeEnemy.gameObject);
        }
        if(enemies.Count > 0)
        {
            foreach (Enemy e in enemies)
            {
                Destroy(e.gameObject);
            }
        }
        enemies.Clear();
    }

    public void CutscenePositions()
    {
        Camera.main.transform.position = new Vector3(-34.18f, -1.2f, Camera.main.transform.position.z);
        Samurai.instance.transform.position = new Vector3(-31.33f, Samurai.instance.transform.position.y, Samurai.instance.transform.position.z);
    }

    public void StartGame()
    {
        timeline.Stop();
        gameStart = true;
        enemies[0].active = true;
        activeEnemy = enemies[0]; 
        foreach (GameObject g in uiToDisable)
        {
            g.SetActive(false);
        }
    }

    public void ActivateNextEnemy()
    {
        if(enemies.Count <= 0)
        {
            WinGame();
        }
        else
        {
            enemies[0].active = true;
            activeEnemy = enemies[0];
        }
    }

    public void UnpauseTimeline()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void PauseTimeline()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    
    public void PauseTimelineClickResume()
    {
        clickToResume = true;
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void SetupGamePositions()
    {
        ClearEnemies();
        Camera.main.transform.position = new Vector3(-3.2f, -1.4f, Camera.main.transform.position.z);
        Samurai.instance.transform.position = new Vector3(-8.25f, Samurai.instance.transform.position.y, Samurai.instance.transform.position.z);
        Samurai.instance.currentHits = Samurai.instance.maxHits;
        Samurai.instance.alive = true;
        Samurai.instance.comboIndex = 1;
        float currX = spawnstartx;

        for (int i = 0; i < startEnemyAmount; i++)
        {
            var enemy = Instantiate(
                enemyTypes[Random.Range(0, enemyTypes.Length)],
                new Vector3(currX + Random.Range(spawnOffset-0.8f, spawnOffset), -4.51f, 0),
                Quaternion.identity).GetComponent<Enemy>();
            currX = enemy.transform.position.x;
            enemies.Add(enemy);
        }

        RainFollow.instance.follow = true;
        Camera.main.GetComponent<CameraFollow>().cameraFollow = true; 
        
    }

    public void PauseDeathTimeline()
    {
        Samurai.instance.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void PlayDeathTimeline()
    {
        Samurai.instance.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void WinGame()
    {
        Samurai.instance.alive = false;
        endGameTimeline.Play();
    }

    public void PauseGameEndTimeline()
    {
        endGameTimeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void UnpauseGameEndTimeline()
    {
        endGameTimeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

}

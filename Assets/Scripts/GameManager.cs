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

    //UI
    public GameObject[] uiToDisable;

    public bool clickToResume;
    
    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        gm = this;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipCutscene();
        }

        if(activeEnemy != null)
        {
            activeEnemy.active = true;
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
        foreach (Enemy e in enemies)
        {
            enemies.Remove(e);
            Destroy(e.gameObject);
        }
    }

    public void CutscenePositions()
    {
        Camera.main.transform.position = new Vector3(-34.18f, -1.2f, Camera.main.transform.position.z);
        Samurai.instance.transform.position = new Vector3(-31.33f, Samurai.instance.transform.position.y, Samurai.instance.transform.position.z);
    }

    public void StartGame()
    {
        timeline.Stop();
        enemies[0].active = true;
        activeEnemy = enemies[0];
    }

    public void ActivateNextEnemy()
    {
        enemies[0].active = true;
        activeEnemy = enemies[0];
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
        Camera.main.transform.position = new Vector3(-3.2f, -1.4f, Camera.main.transform.position.z);
        Samurai.instance.transform.position = new Vector3(-8.25f, Samurai.instance.transform.position.y, Samurai.instance.transform.position.z);
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
        RainFollow.instance.follow = true;
        Camera.main.GetComponent<CameraFollow>().cameraFollow = true;
    }

}

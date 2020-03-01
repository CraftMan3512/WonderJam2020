using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    private int difficulty;
    private float timeSinceLastSpawn;
    private float timeSinceLastDifUp;
    List<Transform> spawnPoints;
    public float waveTimer;
    public float timeSinceWaveStarted;
    TextMeshProUGUI incoming;
    private bool lastAlive;

    private int maxEnemies = 150*PlayerSpawner.playerCount;
    private int enemiesSpawned = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        incoming = GameObject.Find("Incoming").GetComponent<TextMeshProUGUI>();
        incoming.enabled = false;
        timeSinceWaveStarted = 61;
        spawnPoints = new List<Transform>();
            for(int i = 0; i < GameObject.Find("SpawnPoints").transform.childCount; i++)
            {
                spawnPoints.Add(GameObject.Find("SpawnPoints").transform.GetChild(i));
            }
        difficulty = 1;//TROLLED ANTOIN LOLOLOOLL      
    }

    // Update is called once per frame
    void Update()
    {
        if (!lastAlive)
        {
            if (timeSinceWaveStarted < 60f)
            {
                if (timeSinceLastSpawn >= 5f / difficulty && enemiesSpawned < maxEnemies)
                {
                        enemiesSpawned++;
                        int spawnNumber = Random.Range(0, spawnPoints.Count);
                        GameObject enemy = Instantiate(Resources.Load<GameObject>("Enemy"+(int)Random.Range(1,3)), new Vector3(spawnPoints[spawnNumber].position.x, spawnPoints[spawnNumber].position.y, spawnPoints[spawnNumber].position.z), Quaternion.identity);
                        enemy.GetComponent<EnemyMovement>().speed = (float)difficulty / 6 + 1;
                        enemy.GetComponent<EnemyAI>().damage += difficulty / 5 + 1;
                        enemy.GetComponent<EnemyAI>().Hp = 10*( difficulty / 5 + 1);
                        timeSinceLastSpawn = 0f;
                }
                else
                {
                    timeSinceLastSpawn += Time.deltaTime;
                }
                
                timeSinceWaveStarted += Time.deltaTime;
                incoming.SetText("Zombies incoming! Hold for " + (60 - (int)timeSinceWaveStarted) + " more seconds!");

            }
            else
            {

                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveTimer <= 0f)
                {
                    waveTimer = 30f;//TIMER DE DEPART
                    incoming.enabled = true;
                }
                else
                {
                    incoming.SetText("Kill the remaining zombies!");
                    if (waveTimer > 0f)
                    {

                        incoming.SetText("The next wave is coming in " + (int)waveTimer + " seconds. Quick! Prepare your defenses!");
                        waveTimer -= Time.deltaTime;
                        if (waveTimer <= 0f)
                        {
                            waveTimer = -5f;
                            timeSinceWaveStarted = 0f;
                            //next wave starts
                            enemiesSpawned = 0;
                        }
                    }



                }


            }
            if (timeSinceLastDifUp > difficulty * 1.5f/PlayerSpawner.playerCount)
            {
                difficulty++;
                timeSinceLastDifUp = 0f;
            }
            else
            {
                timeSinceLastDifUp += Time.deltaTime;
            }
        }
        else
        {
            
            if (timeSinceLastSpawn >= 5f / difficulty && enemiesSpawned < maxEnemies)
            {

                    int spawnNumber = Random.Range(0, spawnPoints.Count);
                    enemiesSpawned++;
                    GameObject enemy = Instantiate(Resources.Load<GameObject>("Ninja"), new Vector3(spawnPoints[spawnNumber].position.x, spawnPoints[spawnNumber].position.y, spawnPoints[spawnNumber].position.z), Quaternion.identity);
                    enemy.GetComponent<EnemyMovement>().speed = (float)difficulty / 6 + 1;
                    enemy.GetComponent<EnemyAI>().Hp = 10*( difficulty / 3 + 1);
                    enemy.GetComponent<EnemyAI>().damage += difficulty / 3 + 1;
                    timeSinceLastSpawn = 0f;
                    if(enemiesSpawned == maxEnemies)
                    {
                    lastAlive = false;
                    maxEnemies = 150*PlayerSpawner.playerCount;
                    enemiesSpawned = 0;
                    difficulty = 10;
                    }
            }
            else
            {
                timeSinceLastSpawn += Time.deltaTime;
            }
        }
        



    }


    public void LastAlive()
    {
        enemiesSpawned = 0;
        maxEnemies = 20*PlayerSpawner.playerCount;
        lastAlive = true;
        incoming.SetText("With only one player remaining, the zombies are sending their specialized assassination group! Brace yourself!");
        difficulty = 50;
    }
}

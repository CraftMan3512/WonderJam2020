using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int difficulty;
    private float timeSinceLastSpawn;
    private float timeSinceLastDifUp;
    List<Transform> spawnPoints;
    public float waveTimer;
    public float timeSinceWaveStarted;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceWaveStarted = 61;
        spawnPoints = new List<Transform>();
            for(int i = 0; i < GameObject.Find("SpawnPoints").transform.childCount; i++)
            {
                spawnPoints.Add(GameObject.Find("SpawnPoints").transform.GetChild(i));
            }
        difficulty = 1;
        float angle = Random.Range(0, 360);
        //GameObject enemy = Instantiate((GameObject)Resources.Load("Enemy"), new Vector3(35 * Mathf.Cos(angle * Mathf.Deg2Rad), 25 * Mathf.Sin(angle * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceWaveStarted < 60)
        {
            if (timeSinceLastSpawn >= 5f / difficulty)
            {
                for (int i = 0; i < 1; i++) //je garde la boucle si on veut changer comment le scaling marche :)
                {
                    int spawnNumber = Random.Range(0, spawnPoints.Count);
                    GameObject enemy = Instantiate(Resources.Load<GameObject>("Enemy"), new Vector3(spawnPoints[spawnNumber].position.x, spawnPoints[spawnNumber].position.y, spawnPoints[spawnNumber].position.z), Quaternion.identity);
                    //GameObject enemy = Instantiate((GameObject)Resources.Load("Enemy"), new Vector3(35 * Mathf.Cos(angle * Mathf.Deg2Rad), 25 * Mathf.Sin(angle * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);
                    enemy.GetComponent<EnemyMovement>().speed = (float)difficulty / 3 + 1;
                    enemy.GetComponent<EnemyAI>().Hp *= difficulty / 5 + 1;
                }
                timeSinceLastSpawn = 0;
            }
            else
            {
                timeSinceLastSpawn += Time.deltaTime;
            }

            if (timeSinceLastDifUp > difficulty * 2)
            {
                difficulty++;
                timeSinceLastDifUp = 0;
            }
            else
            {
                timeSinceLastDifUp += Time.deltaTime;
            }
            timeSinceWaveStarted += Time.deltaTime;
           
        }
        else
        {
            
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveTimer == 0)
            {
                waveTimer = 30;
            }
            if(waveTimer > 0)
            {
                waveTimer -= Time.deltaTime;
            }
            else
            {
                waveTimer = 0;
                timeSinceWaveStarted = 0;
            }
            
        }



    }
}

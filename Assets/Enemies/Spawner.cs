using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int difficulty;
    private float timeSinceLastSpawn;
    private float timeSinceLastDifUp;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 1;
        float angle = Random.Range(0, 360);
        GameObject enemy = Instantiate((GameObject)Resources.Load("Enemy"), new Vector3(10 * Mathf.Cos(angle * Mathf.Deg2Rad), 10 * Mathf.Sin(angle * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastSpawn >= 5f / difficulty)
        {
            for(int i = 0; i <  1; i++) //je garde la boucle si on veut changer comment le scaling marche :)
            {
                float angle = Random.Range(0, 360);
                GameObject enemy = Instantiate((GameObject)Resources.Load("Enemy"), new Vector3(10 * Mathf.Cos(angle * Mathf.Deg2Rad), 10 * Mathf.Sin(angle * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);
            }
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }

        if(timeSinceLastDifUp > difficulty*2)
        {
            difficulty++;
            timeSinceLastDifUp = 0;
        }
        else
        {
            timeSinceLastDifUp += Time.deltaTime;
        }

    }
}

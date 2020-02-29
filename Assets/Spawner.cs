using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int difficulty;
    private float timeSinceLastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastSpawn >= 5f / difficulty)
        {
            for(int i = 0; i < difficulty; i++)
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
    }
}

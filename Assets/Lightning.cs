using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float range;
    // Start is called before the first frame update
    void Start()
    {
     if(range == 0)
        {
            range = 1;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Strike()
    {
     foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(Vector2.Distance(transform.position,enemy.transform.position) < range)
            {
                Vector3[] points = new Vector3[5];
                points[0] = transform.position;              
                for(int i = 1; i < points.Length; i++)
                {
                  
                    if(i == 4)
                    {
                        points[i] = enemy.transform.position;
                    }
                }
            }

            yield return new  WaitForSeconds(0f);
        }   
    }
}

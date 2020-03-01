using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float range;
    public int damagePerStrike;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>(); 
     if(range == 0)
        {
            range = 1;
        }
        StartCoroutine(Strike());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Strike()
    {
        int iterations = 0;
        GameObject lastEnemy = null;
     foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = 0;
            if (lastEnemy != null)
            {
                distance = Vector2.Distance(transform.position, enemy.transform.position);
            }
            else
            {
                distance = Vector2.Distance(transform.position, enemy.transform.position);
            }
            if (distance < range)
            {
                Vector3[] points = new Vector3[5];
                if (lastEnemy != null)
                {
                    points[0] = lastEnemy.transform.position;
                }
                else
                {
                    points[0] = transform.position;
                }
                for(int i = 1; i < points.Length; i++)
                {
                    
                    if(i == 4)
                    {
                        points[i] = enemy.transform.position;
                    }
                    else
                    {
                        points[i] = points[i - 1] + new Vector3(Random.Range(-1f, 1f) * distance / 5, Random.Range(-1f, 1f) * distance / 5, transform.position.z);
                    }
                }
                lr.positionCount += points.Length;
                for(int i  = 0; i < points.Length; i++)
                {
                    lr.SetPosition(lr.positionCount - 6 + i, points[i]);
                }

                enemy.GetComponent<EnemyAI>().TakeDamage(damagePerStrike);

            }
            
           
            lastEnemy = enemy;
            iterations++;
            break;
           

        }
        
        yield return new WaitForSeconds(0.5f);
        if (iterations == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Strike());
        }

    }
}

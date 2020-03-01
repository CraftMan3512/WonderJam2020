using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float range;
    public int damagePerStrike;
    private LineRenderer lr;
    public Vector3 lastEnemy;
    bool firstStrike;
    private List<GameObject> zappedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        firstStrike = true;
        lastEnemy = Vector3.zero;
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
        GameObject closestEnemy = null;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            bool alreadyZapped = false;
            foreach(GameObject zapped in zappedEnemies)
            {
                if (zapped) {
                    if (enemy == zapped)
                    {
                        alreadyZapped = true;
                    }
                }
            }
            if (!alreadyZapped)
            {
                float distance = 0;
                if (!firstStrike)
                {
                    distance = Vector2.Distance(lastEnemy, enemy.transform.position);
                }
                else
                {
                    distance = Vector2.Distance(transform.position, enemy.transform.position);
                }
                if (distance < range)
                {
                    if (closestEnemy == null)
                    {
                        closestEnemy = enemy;
                    }
                    else
                    {
                        if (!firstStrike)
                        {
                            if (Vector2.Distance(lastEnemy, enemy.transform.position) < Vector2.Distance(lastEnemy, closestEnemy.transform.position))
                            {
                                closestEnemy = enemy;
                            }
                        }
                        else
                        {
                            if (Vector2.Distance(transform.position, enemy.transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                            {
                                closestEnemy = enemy;
                            }
                        }

                    }

                    iterations++;


                }
            }

          



        }
        try
        {
            Vector3[] points = new Vector3[5];
            if (!firstStrike)
            {
                points[0] = lastEnemy;
            }
            else
            {
                firstStrike = false;
                points[0] = transform.position;
            }
            for (int i = 1; i < points.Length; i++)
            {

                if (i == 4)
                {
                    points[i] = closestEnemy.transform.position;
                }
                else
                {
                    //faire qui va dans la direction générale de l'enemy.
                    points[i] = points[i - 1] + new Vector3(Random.Range(0f, 1f)*Mathf.Sign(closestEnemy.transform.position.x -points[i-1].x) * Vector2.Distance(points[i - 1],closestEnemy.transform.position)/4, Random.Range(0f, 1f) * Mathf.Sign(closestEnemy.transform.position.y - points[i - 1].y) * Vector2.Distance(points[i - 1], closestEnemy.transform.position) / 4, transform.position.z);
                }
            }
            lr.positionCount = points.Length;
            lr.SetPositions(points);

            lastEnemy = closestEnemy.transform.position;
            closestEnemy.GetComponent<EnemyAI>().TakeDamage(damagePerStrike);
            zappedEnemies.Add(closestEnemy);
        }catch(System.Exception e) { }

        yield return new WaitForSeconds(0.1f);
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

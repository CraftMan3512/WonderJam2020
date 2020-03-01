using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int damage;
    public GameObject target;
    public float lifeTime;
    public float speed;
    private float attackCooldown;
    private GameObject ply;
    
    public GameObject Ply
    {
        set => ply = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (lifeTime > 0)
        {

            if (target != null)
            {

                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
                float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                if (attackCooldown >= 1)
                {
                    if (Vector2.Distance(transform.position, target.transform.position) < 0.4f)
                    {
                        target.GetComponent<EnemyAI>().TakeDamage(damage);
                        attackCooldown = 0;
                        if (target.gameObject.GetComponent<EnemyAI>().Hp <= 0)
                        {
                            if (!target.gameObject.GetComponent<EnemyAI>().dead)
                            {
                                if (ply)
                                {
                                    ply.GetComponent<Player>()
                                        .addScore(target.gameObject.GetComponent<EnemyAI>().ScorePoints);
                                    target.gameObject.GetComponent<EnemyAI>().dead = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    attackCooldown += Time.deltaTime;
                }

            }
            else
            {
                FindClosestTarget();
            }
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            StartCoroutine(DeathAnim());
        }
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(0.2f);
        float a = GetComponent<SpriteRenderer>().color.a;
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - a / 4);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }


    private void FindClosestTarget()
    {

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (obj != null)
            {
                if (target == null)
                {
                    try //parce que wtf obj yer null
                    {
                        target = obj;                    
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.ToString());
                    }
                }
                else
                {
                    try //parce que wtf obj yer null
                    {

                        if (Vector2.Distance(this.transform.position, obj.transform.position) <
                            Vector2.Distance(this.transform.position, target.transform.position))
                        {
                            target = obj;
                         
                        }

                    }
                    catch (System.Exception e) { Debug.Log(e.ToString()); }

                }

            }
        }
    }
}

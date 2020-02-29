using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

  
    private int hp;
    private int maxHp;
    private int damage;
    private GameObject target;
    private float attackCooldown;

    public int Hp { get => hp; set => hp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Damage { get => damage; set => damage = value; }
    public GameObject Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        damage = 2;
        maxHp = 20;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (attackCooldown >= 2f)
            {
                if (Vector2.Distance(transform.position, target.transform.position) <= 1f)
                {
                    if (target.tag.Equals("Box"))
                    {
                        target.GetComponent<Cube>().TakeDamage(damage);
                        attackCooldown = 0;
                    }
                }
            }
            else
            {
                attackCooldown += Time.fixedDeltaTime;
            }
        }
        else
        {
            GetComponent<EnemyMovement>().TargetClosestPlayer();
        }
    }


    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageEffect());
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageEffect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        
    }


}

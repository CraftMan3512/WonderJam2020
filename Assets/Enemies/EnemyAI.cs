﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

  
    private int hp;
    private int maxHp;
    public int damage;
    private GameObject target;
    private float attackCooldown;
    public float timeBeforeAttack = 0.4f;
    private int scorePoints;
    public bool dead;

    public int ScorePoints
    {
        set => scorePoints = value;
        get => scorePoints;
    }

    public int Hp { get => hp; set => hp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Damage { get => damage; set => damage = value; }
    public GameObject Target { get => target; set => target = value; }
    

    // Start is called before the first frame update
    void Start()
    { 
        //damage = 2;
       // maxHp = 10;
       // hp = maxHp;
        scorePoints = 10;
        
        //coroutine for zombie noises
        StartCoroutine(ZombieNoise());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (attackCooldown >= 2f-timeBeforeAttack)
            {
                if (Vector2.Distance(transform.position, target.transform.position) <= 1f)
                {
                    if (attackCooldown >= 2f)
                    {
                        if (target.tag.Equals("Box"))
                        {
                            target.GetComponent<Cube>().TakeDamage(damage);
                            attackCooldown = 0;
                        }else if (target.tag.Equals("Player"))
                        {
                            
                                target.GetComponent<Player>().TakeDamage(damage); 
                                attackCooldown = 0;
                            
                        }
                    }
                    else
                    {
                        attackCooldown += Time.deltaTime;
                    }
                }
                else
                {
                    attackCooldown = 2f-timeBeforeAttack;
                }


            }
            else
            {
                if (attackCooldown < 2f-timeBeforeAttack)
                {
                    attackCooldown += Time.fixedDeltaTime;
                }
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
        if(hp <= 0&&!dead)//DEATH
        {
            if((int)Random.Range(0,30) == 1)
            {
                Instantiate(Resources.Load<GameObject>("Weapon Crate"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }else if((int)Random.Range(0, 40) == 1)
            {
                Instantiate(Resources.Load<GameObject>("MedKit"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }

            //gore
            if (!dead)
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/ZombieDeath"));
                GameObject blood = Instantiate(Resources.Load<GameObject>("Gore"), transform.position,
                    Quaternion.identity);
                blood.transform.localScale = new Vector3(1, 1, 0) * 0.1f;
            }


            Destroy(gameObject);
        }
    }

    IEnumerator DamageEffect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        
    }

    IEnumerator ZombieNoise()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f,20f));
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/Zombie"));

        }
        
    }


}

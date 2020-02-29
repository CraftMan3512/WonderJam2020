﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    /*
   * notes pour le mouvement -----
   * faire une collision plus grande que l'enemy qui est la range dagro. lorsque ya qqch dedans dagroable, il a une chance de l'agros (si c'est pas un playeur(
   * rajouter un cooldown sur lagro.)) si c'est un wall pis y l'agro pas, y bouge vers la gauche/droite de la distance d'un mur pis y se remet a avancer vers le joueur
   * le plus proche.
   * 
   * */

    public float speed;
    private Transform target;
    private EnemyAI AI;
    private bool dodgingWall;
    private Vector2 dodgeTarget;

    public Transform Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAI>();
        if(speed == 0f)
        {
            speed = 1f;
        }
        //get le player le plus proche comme target;

        TargetClosestPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Target == null)
        {
            TargetClosestPlayer();
        }
        Move();
    }


    private void Move()
    { if (dodgingWall)
        {  //si le playeur atteint le "niveau " de l'enemy, il se remet à aller vers lui.
           if(Vector2.Distance(dodgeTarget, Target.position) > Vector2.Distance(transform.position, Target.position))
            {
                dodgingWall = false;              
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, dodgeTarget, speed * Time.fixedDeltaTime);
                float angle = Mathf.Atan2(dodgeTarget.y - transform.position.y, dodgeTarget.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 ));
            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.fixedDeltaTime);
           
           

            
            float  angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 ));
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Target == null)
        {
            TargetClosestPlayer();
        }
        if (collision.transform != Target) {
            if (collision.tag.Equals("Player")){
                Target = collision.transform;
                AI.Target = collision.gameObject;
            }
            else if(collision.tag.Equals("Box"))
            {
                if(Random.Range(0,100) < 30f)
                {
                    Target = collision.transform;
                    AI.Target = collision.gameObject;
                } else
                {    //si y agro pas la boite, il bouge d'une certain distance vers le playeur en essayant de contourner la boite.
                    if (Mathf.Abs(transform.position.y - Target.position.y) > Mathf.Abs(transform.position.x - Target.position.x))
                    {
                        dodgeTarget = new Vector2(transform.position.x + 5*Mathf.Sign(Target.position.x - transform.position.x), transform.position.y);
                        
                    }
                    else
                    {
                        dodgeTarget = new Vector2(transform.position.x, transform.position.y + 5 * Mathf.Sign(Target.position.y - transform.position.y));
                    }
                    dodgingWall = true;
                }
            }
        }
    }


    public void TargetClosestPlayer()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj != null)
            {
                if (Target == null)
                {
                    try //parce que wtf obj yer null
                    {
                        Target = obj.transform;
                        AI.Target = obj;
                    }
                    catch (System.Exception e)
                    {
                        
                    }
                }
                else
                {

                    if (Vector2.Distance(this.transform.position, obj.transform.position) < Vector2.Distance(this.transform.position, Target.position))
                    {
                        Target = obj.transform;
                        AI.Target = obj;
                    }
                }

            }
        }
    }
    



}

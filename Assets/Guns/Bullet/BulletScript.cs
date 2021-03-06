﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;
    public float angleRdm=3;
    private GameObject ply;
    public bool fire;
    public bool rocket;
    public GameObject nextBullet;

    public GameObject Ply
    {
        get => ply;
        set => ply = value;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        angleRdm=Random.Range(-angleRdm, angleRdm);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100*speed*Mathf.Cos((transform.eulerAngles.z+90+angleRdm)*Mathf.Deg2Rad),100*speed*Mathf.Sin((transform.eulerAngles.z+90+angleRdm)*Mathf.Deg2Rad)));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!fire)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                if (collision.gameObject.GetComponent<EnemyAI>().Hp <= 0)
                {
                    if (!collision.gameObject.GetComponent<EnemyAI>().dead)
                    {
                        ply.GetComponent<Player>().addScore(collision.gameObject.GetComponent<EnemyAI>().ScorePoints);
                        collision.gameObject.GetComponent<EnemyAI>().dead = true;
                    }
                }
                
            }
            else if (collision.gameObject.tag.Equals("Box"))
            {
                collision.gameObject.GetComponent<Cube>().TakeDamage(damage);
                
            }
            if (rocket)
            {
                GameObject rocket = Instantiate(nextBullet,transform.position,Quaternion.identity);
                rocket.GetComponent<RocketScript>().Ply = ply;
                Destroy(gameObject);
            }else
            if (collision.gameObject.tag.Equals("Box") || collision.gameObject.tag.Equals("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fire)
        {
            Collider2D collision = other;
            if (collision.gameObject.tag.Equals("Enemy"))
            {

                collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                if (collision.gameObject.GetComponent<EnemyAI>().Hp <= 0)
                {
                    if (!collision.gameObject.GetComponent<EnemyAI>().dead)
                    {
                        ply.GetComponent<Player>().addScore(collision.gameObject.GetComponent<EnemyAI>().ScorePoints);
                        collision.gameObject.GetComponent<EnemyAI>().dead = true;
                    }
                }
            }
            else if (collision.gameObject.tag.Equals("Box"))
            {
                collision.gameObject.GetComponent<Cube>().TakeDamage(damage);
            }
        }
    }
}

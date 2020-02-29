using System;
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

    public GameObject Ply
    {
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
        if (collision.gameObject.tag.Equals("Enemy"))
        {
           
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            if (collision.gameObject.GetComponent<EnemyAI>().Hp <= 0)
            {
                if(!collision.gameObject.GetComponent<EnemyAI>().dead){
                    ply.GetComponent<Player>().addScore(collision.gameObject.GetComponent<EnemyAI>().ScorePoints);
                    collision.gameObject.GetComponent<EnemyAI>().dead = true;
                }
            }
            
            Destroy(gameObject);
        }else if (collision.gameObject.tag.Equals("Box"))
        {
            collision.gameObject.GetComponent<Cube>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

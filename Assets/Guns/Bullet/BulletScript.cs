using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100*speed*Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad),100*speed*Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad)));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
           
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            Destroy(gameObject);
        }else if (collision.gameObject.tag.Equals("Box"))
        {
            collision.gameObject.GetComponent<Cube>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesScript : MonoBehaviour
{
    public int dmg=5;
    public float distance = 0.2f;
    private CircleCollider2D _circleCollider2D;
    public float speed=0.6f;
    private float startPos;

    private bool movingFwd;
    private bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition.y;
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingFwd)
        {
            _circleCollider2D.enabled = true;
            transform.localPosition+=Vector3.up*Time.deltaTime*speed*1.3f;
            if (transform.localPosition.y - startPos >= distance)
            {
                transform.localPosition = new Vector3(0,distance+startPos);
                movingFwd = false;
                stopped = false;
            }
        }
        if (!stopped)
        {
            _circleCollider2D.enabled = true;
            transform.localPosition+=Vector3.down*Time.deltaTime*speed;
            if (transform.localPosition.y<=startPos)
            {
                transform.localPosition = new Vector3(0,startPos);
                movingFwd = false;
                stopped = true;
            }
        }else
            _circleCollider2D.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(dmg);
            if (collision.gameObject.GetComponent<EnemyAI>().Hp <= 0)
            {
                transform.parent.parent.gameObject.GetComponent<Player>().addScore(collision.gameObject.GetComponent<EnemyAI>().ScorePoints);
            }
        }
    }

    public void Shoot()
    {
        if(stopped){
            movingFwd = true;
        _circleCollider2D.enabled = false;
        }
    }
}

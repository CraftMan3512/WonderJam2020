using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    private LineRenderer lr;
    private GameObject player;
    private Vector3[] positions;
    public float speed = 10f;
    private List<float> angles = new List<float>();
    public bool shooting = true;

    public void SpawnWater(Vector3 pos,float angle, GameObject player)
    {

        this.player = player;
        lr = GetComponent<LineRenderer>();
        AddPoint(pos,angle);
        AddPoint(pos,angle);
        GetComponent<EdgeCollider2D>().offset = new Vector2(-transform.position.x,-transform.position.y);
    }

    public void AddPoint(Vector3 pos,float angle)
    {

        //Rendering of lines
        positions = new Vector3[lr.positionCount+1];
        lr.positionCount++;
        positions[0] = pos;
        for (int i = 1; i < lr.positionCount; i++) positions[i] = lr.GetPosition(i-1);
        lr.SetPositions(positions);
        if (lr.positionCount > 1) angles.Insert(1,angle);
        else angles.Insert(0,angle);

        //to vector2 for collisions
        Vector2[] positions2D = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++) positions2D[i] = positions[i];
        GetComponent<EdgeCollider2D>().points = positions2D;
        
    }

    private void FixedUpdate()
    {
        //line movement
        for (int i = 0; i < lr.positionCount; i++)
        {
            
            //Debug.Log("MOVE WATER " + i + " WITH ANGLE " + angles[i]);
            positions[i] += new Vector3(speed*Mathf.Cos(angles[i]*Mathf.Deg2Rad),speed*Mathf.Sin(angles[i]*Mathf.Deg2Rad),0)*Time.deltaTime;

        }
        
        lr.SetPositions(positions);
        
        //collision movement also
        //to vector2 for collisions
        Vector2[] positions2D = new Vector2[lr.positionCount];
        for (int i = 0; i < positions.Length; i++) positions2D[i] = positions[i];
        GetComponent<EdgeCollider2D>().points = positions2D;


        if (shooting)
        {
            
            //water coming out of gun
            //Debug.Log("PLAYER POS IS " + player.transform.position);
            lr.SetPosition(0,player.transform.position);
            GetComponent<EdgeCollider2D>().points[0] = player.transform.position;   
            
        }

        if (lr.GetPosition(0).x <= -30 || lr.GetPosition(0).x >= 30 || lr.GetPosition(0).y >= 20 || lr.GetPosition(0).y <= -20)
        {
            
            Destroy(gameObject);
            
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag.Equals("Enemy"))
        {
            
            other.GetComponent<EnemyAI>().TakeDamage(3);
            if (other.gameObject.GetComponent<EnemyAI>().Hp <= 0)
            {
                if (!other.gameObject.GetComponent<EnemyAI>().dead)
                {
                    if (player) player.transform.parent.GetComponent<Player>().addScore(other.gameObject.GetComponent<EnemyAI>().ScorePoints);
                    other.gameObject.GetComponent<EnemyAI>().dead = true;
                }
            }
        }
        
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private LineRenderer lr;
    public float strength;
    private float baseAngle;
    private float radius;
    private EdgeCollider2D ec;

    
    // Start is called before the first frame update
    void Start()
    {
        baseAngle = transform.eulerAngles.z+90;
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * 4 * Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.Deg2Rad), 100 * 4 * Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad)));
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] points = new Vector3[30];
        Vector2[] points2d = new Vector2[30];
        lr.positionCount = points.Length;
        for(int i = - 15; i < 15; i++)
        {
            points2d[i + 15] =  new Vector2(radius * Mathf.Cos((baseAngle + i * 4) * Mathf.Deg2Rad), radius * Mathf.Sin((baseAngle + i * 4) * Mathf.Deg2Rad));
            points[i + 15] = transform.position + new Vector3(radius * Mathf.Cos((baseAngle+i*4) * Mathf.Deg2Rad), radius * Mathf.Sin((baseAngle + i * 4) * Mathf.Deg2Rad),0);

        }

        lr.SetPositions(points);
        ec.points = points2d;
        radius += Time.deltaTime*4;

       if(radius > 5)
        {
            Destroy(gameObject);
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag.Equals("Enemy")|| collision.tag.Equals("Player"))
        {
            Debug.Log("Hey");
            Vector2 forceDirection = ((collision.transform.position - transform.position)).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(strength*forceDirection);
        }
    }
}

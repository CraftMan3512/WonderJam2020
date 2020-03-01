using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarThrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float speed;
    private bool spawnedGhosts;
    private float timer;
    void Start()
    {     
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * speed * Mathf.Cos((transform.eulerAngles.z + 90 ) * Mathf.Deg2Rad), 100 * speed * Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad)));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 2)
        {
            if (!spawnedGhosts)
            {
                int amount = Random.Range(4, 10);
                for (int i = 0; i < amount; i++)
                {
                    GameObject ghost = Instantiate(Resources.Load<GameObject>("Ghost"), new Vector3(15 * Mathf.Cos(360f / amount * i * Mathf.Deg2Rad), 10 * Mathf.Sin(360f / amount * i * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);
                    spawnedGhosts = true;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {       
            if (collision.gameObject.tag.Equals("Enemy")) {
            int amount = Random.Range(4, 10);
            if (!spawnedGhosts)
            {
                for (int i = 0; i < amount; i++)
                {

                    GameObject ghost = Instantiate(Resources.Load<GameObject>("Ghost"), new Vector3(15 * Mathf.Cos(360f / amount * i * Mathf.Deg2Rad), 10 * Mathf.Sin(360f / amount * i * Mathf.Deg2Rad), transform.position.z), Quaternion.identity);                   
                    ghost.GetComponent<Ghost>().target = collision.gameObject;
                    Debug.Log(ghost.transform.position);
                }
                spawnedGhosts = true;
            }
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);           
            Destroy(gameObject);
            }
        
    }
}

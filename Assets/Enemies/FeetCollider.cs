using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyAI AI;
    private EnemyMovement move;
    void Start()
    {
      AI = transform.parent.gameObject.GetComponent<EnemyAI>();
      move = transform.parent.gameObject.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Joueur") || collision.tag.Equals("Box"))
        {
            AI.Target = collision.gameObject;
            move.Target = collision.transform;
        }
    }

    
}

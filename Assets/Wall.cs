using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float ySize;
    private float xSize;
    public bool forceRight;
    public bool forceLeft;
    // Start is called before the first frame update
    void Start()
    {
       BoxCollider2D bx = GetComponent<BoxCollider2D>();
       bx.size = GetComponent<SpriteRenderer>().size;
        
        xSize = bx.size.x; //valeur de convertion tile->real world size;
        ySize = bx.size.y;

        if(xSize > ySize)
        {
            xSize += 1.1f;
            ySize += 0.2f;
        }
        else
        {
            ySize += 1.1f;
            xSize += 0.2f;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 Dodge(Transform enemyTrans)
    {
        Vector2 dodgeTarget;
       if(Mathf.Abs(transform.position.x - enemyTrans.position.x) < xSize-0.8f)
        {
            if (xSize > ySize)
            {
                if (forceRight)
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x + xSize - (enemyTrans.position.x - transform.position.x), enemyTrans.position.y);                
                }
                else if (forceLeft)
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x - xSize + (enemyTrans.position.x - transform.position.x), enemyTrans.position.y);
                }
                else
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x + Mathf.Sign(enemyTrans.position.x - transform.position.x) * xSize - (enemyTrans.position.x - transform.position.x), enemyTrans.position.y);
                }
            }
            else
            {
                dodgeTarget = new Vector2(((transform.position.x - enemyTrans.position.x) + Mathf.Sign(Mathf.Sin(enemyTrans.eulerAngles.z - 90)) * xSize), enemyTrans.position.y );
            }
            //faut le contourner en X
        }
        else
        {
            //faut le contourner en Y
            //faut juste faire marcher ça 1!$"/!$/"$/"!$/"!$
            if (xSize > ySize)
            {

                dodgeTarget = new Vector2(enemyTrans.position.x, ((transform.position.y - enemyTrans.position.y) + Mathf.Sign(Mathf.Sin(enemyTrans.eulerAngles.z - 90)) * ySize));
            }
            else
            {
                if (forceRight)
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x, enemyTrans.position.y + ySize - (enemyTrans.position.y - transform.position.y));
                }
                else if (forceLeft)
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x, enemyTrans.position.y - ySize + (enemyTrans.position.y - transform.position.y));
                }
                else
                {
                    dodgeTarget = new Vector2(enemyTrans.position.x, enemyTrans.position.y + Mathf.Sign(enemyTrans.position.y - transform.position.y) * ySize - (enemyTrans.position.y - transform.position.y));
                }
            }
        }

        return dodgeTarget ;
    }

    public bool XSide(Transform enemyTrans)
    {
        if (Mathf.Abs(transform.position.x - enemyTrans.position.x) < xSize - 0.8f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}

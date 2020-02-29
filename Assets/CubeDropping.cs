using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDropping : MonoBehaviour
{
    private GameObject boxChan;
    private bool held;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("box" + GetComponent<Player>().player))
        {
            if (!held)
            {
                
                held = true;
                boxChan =  Instantiate((GameObject)Resources.Load("Box"),new Vector3((int)transform.position.x,(int)transform.position.y,transform.position.z),Quaternion.identity);
                boxChan.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                boxChan.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(HoldingBox());   
                
            }

        }

        if (Input.GetButtonUp("box" + GetComponent<Player>().player))
        {
            held = false;
        }
        
    }

    IEnumerator HoldingBox()
    {
        while (held)
        {
            boxChan.transform.position = new Vector3(Mathf.Round(transform.position.x + 1 * Mathf.Cos((transform.eulerAngles.z+90 ) * Mathf.Deg2Rad)), Mathf.Round(transform.position.y + 1 * Mathf.Sin((transform.eulerAngles.z+90) * Mathf.Deg2Rad)), transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        
        boxChan.transform.position = new Vector3(Mathf.Round(transform.position.x + 1 * Mathf.Cos((transform.eulerAngles.z+90) * Mathf.Deg2Rad)), Mathf.Round(transform.position.y + 1 * Mathf.Sin((transform.eulerAngles.z +90) * Mathf.Deg2Rad)), transform.position.z);
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Box"))
        {
            if(obj != boxChan)
            {
                if(obj.transform.position == boxChan.transform.position)
                {
                    Destroy(boxChan);
                }
            }
        }
        if (boxChan != null)
        {
            boxChan.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            boxChan.GetComponent<BoxCollider2D>().enabled = true;
            //ajouter le cost des box;
        }

    }


}

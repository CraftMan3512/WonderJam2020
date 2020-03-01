using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDropping : MonoBehaviour
{
    private GameObject boxChan;
    private bool held;
    private float timeSinceLastDrop;

    public float TimeSinceLastDrop
    {
        get => timeSinceLastDrop;
        set => timeSinceLastDrop = value;
    }

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
                boxChan = Instantiate((GameObject)Resources.Load("Box"), new Vector3((int)transform.position.x, (int)transform.position.y, transform.position.z), Quaternion.identity);
                boxChan.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                boxChan.GetComponent<BoxCollider2D>().enabled = false;         
                if (timeSinceLastDrop >= 3)
                {
                    boxChan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boxes/Metal");
                    boxChan.GetComponent<Cube>().Hp = 30;
                    StartCoroutine(HoldingBox());
                }
                else if(timeSinceLastDrop >= 2)
                {
                    boxChan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boxes/Pierre");
                    boxChan.GetComponent<Cube>().Hp = 20;
                    StartCoroutine(HoldingBox());
                }
                else if(timeSinceLastDrop >= 1)
                {
                    boxChan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boxes/Bois");
                    boxChan.GetComponent<Cube>().Hp = 10;
                    StartCoroutine(HoldingBox());
                }
                else
                {
                    Destroy(boxChan);
                    held = false;
                }
                
                
                
            }

        }

        if (Input.GetButtonUp("box" + GetComponent<Player>().player))
        {
            held = false;
        }

        if (timeSinceLastDrop < 3&&!held)
        {
            timeSinceLastDrop += Time.deltaTime;
        }
        else if(!held)
            timeSinceLastDrop = 3;
    }

    IEnumerator HoldingBox()
    {
        while (held)
        {
            boxChan.transform.position = new Vector3(Mathf.Round(transform.position.x + 1 * Mathf.Cos((transform.eulerAngles.z+90 ) * Mathf.Deg2Rad)), Mathf.Round(transform.position.y + 1 * Mathf.Sin((transform.eulerAngles.z+90) * Mathf.Deg2Rad)), transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        if(boxChan != null) boxChan.transform.position = new Vector3(Mathf.Round(transform.position.x + 1 * Mathf.Cos((transform.eulerAngles.z+90) * Mathf.Deg2Rad)), Mathf.Round(transform.position.y + 1 * Mathf.Sin((transform.eulerAngles.z +90) * Mathf.Deg2Rad)), transform.position.z);
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
            timeSinceLastDrop = 0;
            
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/PlaceBox"),5f);
            
        }

    }

    
}

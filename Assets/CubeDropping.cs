using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDropping : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate((GameObject)Resources.Load("Box"),new Vector3((int)transform.position.x,(int)transform.position.y,transform.position.z),Quaternion.identity);
        }
    }


}

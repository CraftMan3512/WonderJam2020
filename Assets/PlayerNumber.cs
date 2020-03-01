using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    private GameObject ply;

    public GameObject Ply
    {
        get => ply;
        set => ply = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ply)
        transform.position=ply.transform.position;
        else
        {
            Destroy(gameObject);
        }
    }
}

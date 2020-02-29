﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyBar : MonoBehaviour
{

    private GameObject player;
    
    // Start is called before the first frame update
    void Update()
    {
        if(player!=null)
            transform.localScale=new Vector3(player.GetComponent<CubeDropping>().TimeSinceLastDrop/3,1,1);
    }
    public void setPlayer(GameObject ply)
    {
        player = ply;
    }
    
}

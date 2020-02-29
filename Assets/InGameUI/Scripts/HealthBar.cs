﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        transform.localScale=new Vector3((float)player.GetComponent<Player>().Health/player.GetComponent<Player>().maxHealth,1,1);
    }

    public void setPlayer(GameObject ply)
    {
        player = ply;
    }
}

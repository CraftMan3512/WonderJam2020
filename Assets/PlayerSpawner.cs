﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public static int playerCount = 2;

    public CinemachineTargetGroup targetGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPlayers(playerCount);

    }

    public void SpawnPlayers (int playerNb)
    {
        
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[playerNb];

        for (int i = 1; i <= playerNb; i++)
        {

            Debug.Log("Spawning player " + i);
            GameObject player = Instantiate(playerPrefab,transform.position + (Vector3.right*3*i),Quaternion.identity);
            player.GetComponent<Player>().SetPlayerNumber(i);
            player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/P" + i + "/SpriteP" + i);
            targetGroup.m_Targets[i-1] = new CinemachineTargetGroup.Target();
            targetGroup.m_Targets[i-1].target = player.transform;
            GameObject.Find("HealthBar" + i).GetComponent<HealthBar>().setPlayer(player);
            GameObject.Find("FrenzyBar" + i).GetComponent<FrenzyBar>().setPlayer(player);
            GameObject.Find("BoxesBar" + i).GetComponent<BoxesBar>().setPlayer(player);
            GameObject.Find("FinalCanvas").GetComponent<ScoreBoard>().addPlayer(player,i);
            targetGroup.m_Targets[i - 1].weight = 1f;


        }
        
        
    }

}

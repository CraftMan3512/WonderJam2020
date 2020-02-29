using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public int playerNb;
    public static int playerCount;

    public CinemachineTargetGroup targetGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPlayers(playerNb);

    }

    public void SpawnPlayers (int playerNb)
    {
        
        playerCount = playerNb;
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[playerNb];

        for (int i = 1; i <= playerNb; i++)
        {

            Debug.Log("Spawning player " + i);
            GameObject player = Instantiate(playerPrefab,transform.position + (Vector3.right*3*i),Quaternion.identity);
            player.GetComponent<Player>().SetPlayerNumber(i);
            targetGroup.m_Targets[i-1] = new CinemachineTargetGroup.Target();
            targetGroup.m_Targets[i-1].target = player.transform;


        }
        
        
    }
    


}

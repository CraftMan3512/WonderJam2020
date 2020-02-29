using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public int playerNb;
    public static int playerCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPlayers(playerNb);

    }

    public void SpawnPlayers (int playerNb)
    {
        
        playerCount = playerNb;

        for (int i = 1; i <= playerNb; i++)
        {

            Debug.Log("Spawning player " + i);
            GameObject player = Instantiate(playerPrefab,transform.position + (Vector3.right*3*i),Quaternion.identity);
            player.GetComponent<Player>().SetPlayerNumber(i);

        }
        
    }
    


}

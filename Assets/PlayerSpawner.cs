using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public static int playerCount = 2;
    public GameObject playerNumber;

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
            GameObject temp = Instantiate(playerNumber);
            temp.GetComponent<PlayerNumber>().Ply=player;
            temp.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ("P" + i);
        }
        
        
    }

}

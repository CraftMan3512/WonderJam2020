using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public List<GameObject> players;
    public TextMeshProUGUI scoreP1;
    public TextMeshProUGUI scoreP2;
    public TextMeshProUGUI scoreP3;
    public TextMeshProUGUI scoreP4;
    public int playersThisRound;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        playersThisRound = 0;
    }

    public void addPlayer(GameObject ply,int i)
    {
        players.Add(ply);
        playersThisRound++;
    }

    public void Update()
    {
        if(playersThisRound>0)
            if(players[0]) scoreP1.SetText(players[0].GetComponent<Player>().Score.ToString());
        if(playersThisRound>1)
            if(players[1]) scoreP2.SetText(players[1].GetComponent<Player>().Score.ToString());
        if(playersThisRound>2)
            if(players[2]) scoreP3.SetText(players[2].GetComponent<Player>().Score.ToString());
        if(playersThisRound>3)
            if(players[3]) scoreP4.SetText(players[3].GetComponent<Player>().Score.ToString());
    }
    
}

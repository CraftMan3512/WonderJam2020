using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinningPlayer : MonoBehaviour
{
    public TextMeshProUGUI winner;
    public TextMeshProUGUI winnerScore;
    public TextMeshProUGUI totalScore;
    
    void updateWinner(String win, int scoreWin, int scoreTot)
    {
        winner.SetText("Player " + win + " Won!");
        winnerScore.SetText("score : "+scoreWin);
        totalScore.SetText("score total : "+totalScore);
    }
}

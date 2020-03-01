using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class EndValues : MonoBehaviour
{

    public static int winner = 0;
    public static int winnerScore = 0;
    
    public static void GetGameValues(int gameWinner, int gameScore)
    {

        DeathCounter.playersDead = 0;
        winner = gameWinner;
        winnerScore = gameScore;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public static int playersDead = 0;

    public static void PlayerDied(int playerNb, int score)
    {
        if (playersDead < PlayerSpawner.playerCount)
        {
            playersDead++;
            if (playersDead == PlayerSpawner.playerCount - 1)
            {
                Debug.Log("VAGUE SPECIALE DERNIER JOUEUR!");
            }

        } else ToGameOver(playerNb, score);
        
    }

    private static void ToGameOver(int playerNb, int score)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
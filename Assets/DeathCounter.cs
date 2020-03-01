using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public static int playersDead = 0;

    public static void PlayerDied(int playerNb, int score)
    {
        playersDead++;
        if (playersDead < PlayerSpawner.playerCount)
        {
            if (playersDead == PlayerSpawner.playerCount - 1)
            {
                GameObject.Find("WaveSpawner").GetComponent<Spawner>().LastAlive();
            }

        } else ToGameOver(playerNb, score);
        
    }

    private static void ToGameOver(int playerNb, int score)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
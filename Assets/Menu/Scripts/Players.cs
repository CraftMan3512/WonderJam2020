using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Players : MonoBehaviour
{

    // Update is called once per frame
    public void startGame(int nbJoueurs)
    {
        Debug.Log("Nombre de Joueur" + nbJoueurs);
        
        //changer de scene et set les joueurs
        PlayerSpawner.playerCount = nbJoueurs;
        SceneManager.LoadScene("CopyMenu");

    }
}

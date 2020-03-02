using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Players : MonoBehaviour
{
    public bool p1OnKeyboard = false;
    // Update is called once per frame
    public void startGame(int nbJoueurs)
    {
        Debug.Log("Nombre de Joueur" + nbJoueurs);
        Player.keyboard = p1OnKeyboard;
        //changer de scene et set les joueurs
        PlayerSpawner.playerCount = nbJoueurs;
        SceneManager.LoadScene("CopyMenu");
    }

    public void p1OnKeyboardChanged(bool newValue)
    {
        p1OnKeyboard = !p1OnKeyboard;
        Debug.Log(p1OnKeyboard);
    }
}

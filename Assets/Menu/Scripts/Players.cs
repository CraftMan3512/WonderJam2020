using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void startGame(int nbJoueurs)
    {
        Debug.Log("Nombre de Joueur" + nbJoueurs);
    }
}

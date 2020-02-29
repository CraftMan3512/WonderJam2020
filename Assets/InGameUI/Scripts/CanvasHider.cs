using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasHider : MonoBehaviour
{

    public int nbPlayers;
    public GameObject canvasp2;
    public GameObject canvasp3;
    public GameObject canvasp4;

    
    // Start is called before the first frame update
    void Start()
    {
        nbPlayers = PlayerSpawner.playerCount;
        changeNbPlayers(nbPlayers);
    }

    void changeNbPlayers(int nb)
    {
        if (nb == 1)
        {
            canvasp2.SetActive(false);
            canvasp3.SetActive(false);
            canvasp4.SetActive(false);
        }
        else if (nb == 2)
        {
            canvasp2.SetActive(true);
            canvasp3.SetActive(false);
            canvasp4.SetActive(false);
        }
        else if (nb == 3)
        {
            canvasp2.SetActive(true);
            canvasp3.SetActive(true);
            canvasp4.SetActive(false);
        }
        else if (nb == 4)
        {
            canvasp2.SetActive(true);
            canvasp3.SetActive(true);
            canvasp4.SetActive(true);
        }
    }
}

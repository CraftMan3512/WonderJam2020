using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<TextMeshProUGUI>().text = "Player " + EndValues.winner + " won!";

    }
}

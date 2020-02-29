using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    public TextMeshProUGUI scoreP1;
    public TextMeshProUGUI scoreP2;
    public TextMeshProUGUI scoreP3;
    public TextMeshProUGUI scoreP4;

    private int[] scorePlayer;

    // Start is called before the first frame update
    void Start()
    {
        scorePlayer = new int[4];
        for (int i = 0; i < 4; i++)
        {
            scorePlayer[i] = 0;
        }
        scoreP1.SetText(scorePlayer[0].ToString());
        scoreP2.SetText(scorePlayer[1].ToString());
        scoreP3.SetText(scorePlayer[2].ToString());
        scoreP4.SetText(scorePlayer[3].ToString());
        
        updateScore1(457);
    }

    void updateScore1(int scoreSuppl)
    {
        scorePlayer[0] += scoreSuppl;
        scoreP1.SetText(scorePlayer[0].ToString());
    }
    void updateScore2(int scoreSuppl)
    {
        scorePlayer[1] += scoreSuppl;
        scoreP2.SetText(scorePlayer[1].ToString());

    }
    void updateScore3(int scoreSuppl)
    {
        scorePlayer[2] += scoreSuppl;
        scoreP3.SetText(scorePlayer[2].ToString());

    }
    void updateScore4(int scoreSuppl)
    {
        scorePlayer[3] += scoreSuppl;
        scoreP4.SetText(scorePlayer[3].ToString());

    }
}

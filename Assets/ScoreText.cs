using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + ScoringSystem.score;
        if (ScoringSystem.isWin){
        	text.text = "Rabbits Win!";
        }
        if (CountRabbit.isWin){
        	text.text = "Farmer Win!";
        }
    }
}

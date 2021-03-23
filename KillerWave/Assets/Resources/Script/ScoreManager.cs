using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int playerScore;
    public int PlayerScore
    {
        get
        {
            return playerScore;
        }
    }
    public void ResetScore()
    {
        playerScore = 000000000;
    }
    public void SetScore(int incomingScore)
    {
        playerScore += incomingScore;
    }
}

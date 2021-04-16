using UnityEngine;
using UnityEngine.UI;

 public class ScoreManager : MonoBehaviour 
 {
    public static int playerScore;
    public int PlayersScore
    {
        get
        {
            return playerScore;
        }
    }
	
	  public void SetScore(int incomingScore)
    {
        playerScore += incomingScore;
    }
    public void ResetScore()
    {
        playerScore = 000000000;
        if(GameObject.Find("score"))
        {
            GameObject.Find("score").GetComponent<Text>().text = playerScore.ToString();
        }
    }
}
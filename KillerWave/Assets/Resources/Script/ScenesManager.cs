﻿using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    float gameTimer = 0;
    float[] endLevelTimer = { 30, 30, 45 };
    int currentSceneNumber = 0;
    bool gameEnding = false;

  Scenes scenes;
  public enum Scenes
  {
    bootUp,
    title,
    shop,
    level1,
    level2,
    level3,
    gameover
  }
  
    public void ResetScene()
  {
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
  }
  
    public void GameOver()
  {
	Debug.Log("ENDSCORE:" + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
    SceneManager.LoadScene("gameOver");
  }
  
    public void BeginGame()
  {
    SceneManager.LoadScene("testLevel");
  }
    private void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }
    void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }
    void GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level1: case Scenes.level2: case Scenes.level3:
                {
                    if (gameTimer <endLevelTimer[currentSceneNumber-3])
                    {
                        gameTimer += Time.deltaTime;
                    }
                    else
                    {
                        if (!gameEnding)
                        {
                            gameEnding = true;
                            if (SceneManager.GetActiveScene().name != "level3")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().LevelEnds = true;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().GameCompleted = true;
                            }
                            Invoke("NextLevel", 4);
                        }
                    }
                 break;
                }
        }
    }
    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }
    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        GetComponent<GameManager>().SetLivesDisplay(GameManager.playerLives);
        if (GameObject.Find("score"))
        {
            GameObject.Find("score").GetComponent<Text>().text = ScoreManager.playerScore.ToString();
        }
    }
}
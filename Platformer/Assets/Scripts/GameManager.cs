using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // HudManager
    public HudManager hudManager;

    // score of the Player
    public int score = 0;

    //high score of the game
    public int highScore = 0;

    //current level
    public int currentLevel = 0;

    //how many levels there area
    public int highestLevel = 3;

    //static instance of the GameManager that can be accessed from anywhere
    public static GameManager instance;

    public string gameOverReason;

    //goal sound
    public AudioSource goalSound;

    void Awake()
    {
        //check that it exists
        if (instance == null)
        {
            //assign it to the current object
            instance = this;
        }

        //make sure that it is equal to the current object
        else if (instance != this){
            // destroy the current game object -> we only need one and we already have it
            Destroy(gameObject);
        }

        //dont destroy object when changing scenes
        DontDestroyOnLoad(gameObject);

    }

    //increase the player score
    public void IncreaseScore(int amount)
    {
        //increase the score by the amount
        score += amount;
        hudManager.ResetScore();

        if (score > highScore)
        {
            //save a new highscore
            highScore = score;
            hudManager.ResetHighScore();
        }
    }
    
    //game over
    public void GameOver(string reason)
    {
        this.gameOverReason = reason;
        SceneManager.LoadScene("GameOver");
    }

    // reset the game
    public void ResetGame()
    {
        // reste our score
        score = 0;

        //set the current level to 1
        currentLevel = 1;

        //reset hud
        hudManager.ResetScore();

        //load the level1
        SceneManager.LoadScene("Level1");
    }

    //send the player to the next level
    public void IncreaseLevel()
    {
        goalSound.Play();
        // check if highest level
        if (currentLevel < highestLevel)
        {
            currentLevel ++;
        }
        else
        {
            // we are gone go back to level 1
            currentLevel = 1;
        }
        SceneManager.LoadScene("Level" + currentLevel);
        if (currentLevel == 1)
        {
            //hudManager.ShowMessage("Collect the coins and avoid the skulls!", 3f);
        }
        else if (currentLevel == 2)
        {
            //hudManager.ShowMessage("Watchout for traps!", 3f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        currentLevel = 1;
        //hudManager.ShowMessage("Collect the coins and avoid the skulls!", 3f);
    }


    public void StartDemo()
    {
        currentLevel = 0;
        SceneManager.LoadScene("Demo");
    }
}

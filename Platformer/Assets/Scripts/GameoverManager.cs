using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour {

    // access to the text element that shows the score value
    public Text scoreValue;

    // access to high score value
    public Text highScoreValue;

    // why you ded
    public Text reason;

    public AudioSource themeSong;
    void Start()
    {
        themeSong.Play();
        // set the "text" property of our Reason value
        reason.text = GameManager.instance.gameOverReason;

        // set the "text" property of our Score value
        scoreValue.text = GameManager.instance.score.ToString();

        //set the "text" property of our Highscore value
        highScoreValue.text = GameManager.instance.highScore.ToString();
    }


    //send the player back to level 1
    public void RestartGame()
    {
        //reset the game
        GameManager.instance.ResetGame();
    }
}

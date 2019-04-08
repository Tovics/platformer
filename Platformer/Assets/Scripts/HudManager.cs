using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    // score text label
    // public Text scoreLabel;

    public Text scoreValue;

    // public Text highScoreLabel;

    public Text highscoreValue;

    // instruction to player
    public Text message;

	// Use this for initialization
	void Start () {
        // initial message disabled
        message.enabled = false;
        //to start with a fresh score
        ResetScore();
	}
	
	// show up to date stats of the player
    public void ResetScore()
    {
        scoreValue.text = GameManager.instance.score.ToString();
    }

    public void ShowMessage(string messageToPlayer, float delay)
    {
        message.enabled = true;
        message.text = messageToPlayer;
        Invoke("DisableText", delay);
    }

    void DisableText()
    {
        message.enabled = false;
    }

    public void ResetHighScore()
    {
        //highScoreLabel.text = "Highscore: " + GameManager.instance.highScore;
        highscoreValue.text = GameManager.instance.score.ToString();
    }
}

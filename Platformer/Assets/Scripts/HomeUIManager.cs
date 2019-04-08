using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour {

    //start on level 1
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StartDemo()
    {
        SceneManager.LoadScene("Demo");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerLives = 100;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("playerLives", playerLives);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public int playerLives;
    public int lifeCounter;
    public TextMeshProUGUI text;
    public GameObject gameOverScreen;
    public CharacterController2D player;
    public int thresold;
    public HealthBar healthbar;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        lifeCounter = PlayerPrefs.GetInt("playerLives");
        print("Starting health" + lifeCounter);
        healthbar.SetHealth(lifeCounter);
        print("playerLife" + lifeCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter <= 0)
        {
            SceneManager.LoadScene(4);
            player.gameObject.SetActive(false);
        }
    }

     void FixedUpdate()
    {
        if (thresold - 0.25 < player.transform.position.y && player.transform.position.y < thresold)
            lifeCounter = lifeCounter - 25;
            healthbar.SetHealth(lifeCounter);
            PlayerPrefs.SetInt("playerLives", lifeCounter);
    }

    public void TakeDamage(int damage)
    {
        lifeCounter = lifeCounter - 25;
        healthbar.SetHealth(lifeCounter);
        PlayerPrefs.SetInt("playerLives", lifeCounter);
    }
}

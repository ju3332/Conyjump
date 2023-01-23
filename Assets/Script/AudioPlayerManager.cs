using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayerManager : MonoBehaviour
{
    private static AudioPlayerManager instance = null;
    public AudioSource music;
    public AudioClip happyBrithday;
    public AudioClip conyLaugh;
    public AudioClip gameOver;
    private bool playedOnce= false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        music = GetComponent<AudioSource>();
        if (!playedOnce)
            music.Play();
    }

    void Update()
    {
        //End Game play happy brithday song
        if(SceneManager.GetActiveScene().name == SceneManager.GetSceneByName("EndGame").name)
        {
            music.Stop();
            if(!playedOnce)
            {
                AudioSource.PlayClipAtPoint(happyBrithday, transform.position);
                playedOnce = true;
            }
        }

        //GameOver play gameover music
        if (SceneManager.GetActiveScene().name == SceneManager.GetSceneByName("GameOver").name)
        {
            music.Stop();
            if (!playedOnce)
            {
                AudioSource.PlayClipAtPoint(gameOver, transform.position);
                playedOnce = true;
            }
        }

        if((SceneManager.GetActiveScene().name == SceneManager.GetSceneByName("Scene1").name) && (playedOnce))
        {
            music.Stop();
            music.Play();
            playedOnce = false;
        }
    }
}

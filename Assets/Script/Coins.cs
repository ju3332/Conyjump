using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinValue = 1;

    //Sound effect
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.ChangeScore(coinValue);
            AudioSource.PlayClipAtPoint(coinSound,transform.position);
        }

    }
}

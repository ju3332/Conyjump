using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    public int bulletDamage = 20;
    CharacterController2D player;
    Vector2 moveDirection;
    private bool m_FacingLeft = true;
    public GameObject fireSpark;

    //Bullet impact sound
    public AudioClip bulletImpactSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<CharacterController2D>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, transform.position.y);
               
    }

    // Update is called once per frame
    void Update()
    {

        if (moveDirection.x > 0 && m_FacingLeft)
        {
            // ... flip the bullet.
            m_FacingLeft = !m_FacingLeft;
            //fireSpark.transform.Rotate(0f, 180f, 0f);
            transform.Rotate(0f, 180f, 0f);
            
        }

        else if (moveDirection.x < 0 && !m_FacingLeft)
        {
            // ... flip the bullet
            m_FacingLeft = !m_FacingLeft;
            //fireSpark.transform.Rotate(0f, 180f, 0f);
            transform.Rotate(0f, 180f, 0f);
            
        }
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        if (hitinfo.tag == "Player")
        {
            LifeManager.instance.TakeDamage(bulletDamage);
            AudioSource.PlayClipAtPoint(bulletImpactSound, transform.position);
            Destroy(gameObject);
        }
    }
}

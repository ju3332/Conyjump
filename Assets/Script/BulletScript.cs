using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 20;

    //Bullet impact sound
    public AudioClip bulletImpactSound;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Enemy enemy = hitinfo.GetComponent<Enemy>();

        AudioSource.PlayClipAtPoint(bulletImpactSound, transform.position);

        if (enemy !=null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

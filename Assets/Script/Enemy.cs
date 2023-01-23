using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
	Vector3 localscale;

	//Enemy Health info
	public float health;
	public int enemyAttackDamage = 25;
	public GameObject deathEffect;
	public GameObject enemyHealthBar;
	public Transform explosionPoint;

	//Define Player object and enemy move speed and direction
	public Transform Player;
	Transform mytransform;
	private bool m_FacingLeft = true;
	public int moveSpeed;
	private float range;
	private float enemyMovingDirection;
	private float positiveMinDistance = 1f;
	private float negativeMinDistance = -1f;

	//Enemy bullet
	public GameObject bullet;
	public GameObject fireEffect;
	public Transform firePoint;
	Transform fireSparkTransform;
	private SpriteRenderer fireSparkSpriteRenderer;
	float fireRate;
	float nextFire;

	//Enemy foot step
	public AudioSource audioSource;
	private bool IsMoving;

	//Enemy destory sound
	public AudioClip explosionSound;

	void Start()
	{
		mytransform = transform;
		localscale = enemyHealthBar.transform.localScale;
		fireSparkTransform = fireEffect.transform;

		//Fire rate
		fireRate = 5f;
		nextFire = Time.time;

		//Get audio source
		audioSource = gameObject.GetComponent<AudioSource>();

		//Get firespark SpriteRenderer
		fireSparkSpriteRenderer = fireSparkTransform.GetComponent<SpriteRenderer>();
	}

	void Update()
    {
		range = (transform.position - Player.position).magnitude;
		enemyMovingDirection = Player.position.x - mytransform.position.x;

		//Follow player
		if (range > positiveMinDistance || range < negativeMinDistance)
		{
			Debug.Log(range);

			transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);

			if (enemyMovingDirection > 0 && m_FacingLeft)
			{
				// ... flip the player.
				Flip();
				fireSparkSpriteRenderer.flipX = true;
			}

			else if (enemyMovingDirection < 0 && !m_FacingLeft)
			{
				// ... flip the player.
				Flip();
				fireSparkSpriteRenderer.flipX = false;
			}
		}

		if (transform.name == "MonD_01")
		{
			localscale.x = health/25;
		}
		else if (transform.name == "Boss")
        {
			localscale.x = health / 125;
		}
		enemyHealthBar.transform.localScale = localscale;

		//Fire
		checkIfTimeToFire();

		//Foot step sound
		if (transform.position.x != 0) 
			IsMoving = true; // better use != 0 here for both directions
		else 
			IsMoving = false;

		if (IsMoving && !audioSource.isPlaying) 
			audioSource.Play(); // if player is moving and audiosource is not playing play it
		if (!IsMoving) 
			audioSource.Stop(); // if player is not moving and audiosource is playing stop it

	}

	public void checkIfTimeToFire()
    {
		if(Time.time>nextFire && (enemyMovingDirection > (positiveMinDistance + 2f) || enemyMovingDirection < (negativeMinDistance - 2f)))
        {
			GameObject fireSparkClone = Instantiate(fireEffect, firePoint.position, Quaternion.identity);
			Destroy(fireSparkClone, 0.1f); 
			
			Instantiate(bullet, firePoint.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
        }
    }
    public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Instantiate(deathEffect, explosionPoint.position, explosionPoint.rotation);
		AudioSource.PlayClipAtPoint(explosionSound, transform.position);
		Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
		{
			LifeManager.instance.TakeDamage(enemyAttackDamage);
		}
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingLeft = !m_FacingLeft;

		//Rotate
		mytransform.Rotate(0f,180f,0f);
		fireSparkTransform.Rotate(180f, 0f, 0f);
	}
}
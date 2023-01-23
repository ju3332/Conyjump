using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;

	//Run speed
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
	//Move to next scene
	private int nextSceneToLoad;

	//Falldown threshold
	public float threshold;

	//Enemy object
	private GameObject enemy;

	//Boss object
	private GameObject boss;

	//Cony jump and grounded sound
	public AudioClip conyJumpSound;
	public AudioClip conyGroundedSound;

	//Grounded check
	private bool isGrounded = false;

	//Joystick
	public Joystick joystick;

	void Start()
	{
		nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
		enemy = GameObject.Find("MonD_01");
		boss = GameObject.Find("Boss");
	}

	// Update is called once per frame
	void Update()
	{
		if (controller.m_Grounded && !isGrounded) // just hit the ground
		{
			print(controller.m_Grounded);
			AudioSource.PlayClipAtPoint(conyJumpSound, transform.position);
		}
		else if (isGrounded && !controller.m_Grounded) // just left the ground
		{
			print(controller.m_Grounded);
			AudioSource.PlayClipAtPoint(conyJumpSound, transform.position);
		}

		isGrounded = controller.m_Grounded;

		//horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(joystick.Horizontal >= .4f)
        {
			horizontalMove = runSpeed;

		} else if (joystick.Horizontal <= -.4f)
        {
			horizontalMove = -runSpeed;
		} else
        {
			horizontalMove = 0f;
		}

		//horizontalMove = joystick.Horizontal * runSpeed;

		float verticalMove = joystick.Vertical;

/*		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}*/

		if (verticalMove >= .10f && controller.m_Grounded && transform.position.y < 2)
		{
			jump = true;
		}

/*		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}*/
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag("Coins"))
        {
			Destroy(other.gameObject);
        }

		if((other.gameObject.CompareTag("MoveToNextScene") && enemy == false))
        {
			SceneManager.LoadScene(nextSceneToLoad);
		}

		if(other.gameObject.CompareTag("EndGame") && boss == false)
        {
			SceneManager.LoadScene(5);
		}
    }

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;

		if (transform.position.y < threshold)
			transform.position = new Vector3(-7, 2, 0);

	}
}
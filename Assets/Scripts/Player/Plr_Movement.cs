using UnityEngine;
using System.Collections;

public class Plr_Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    public float jumpHight = 3f;

    public float gravity = -9.18f;
    public float groundDis = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    float x;
    float y;
    private Vector3 velocity;
    private bool isGrounded;

    private AudioManager audioManager;

	private void Start()
	{
        audioManager = AudioManager.instance;

        Cursor.lockState = CursorLockMode.Locked;
    }

	private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);

		if (isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
            velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
		}

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private IEnumerator PlayFootSound()
	{
		if (x != 0 || y != 0)
		{
            yield return new WaitForSeconds(0.2f);
            audioManager.Play("FootStep");
		}
	}
}

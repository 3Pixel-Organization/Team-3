using UnityEngine;

public class Plr_Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    void LateUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    }
}

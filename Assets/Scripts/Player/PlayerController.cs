using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jump;
    private Vector2 move;
    private Vector2 mouseMove;

    private PlayerMotor motor;
    private InputSystem controller;

    private float sensitivity = 50f;
    private float sensMultiplier = 0.5f;

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
        controller = new InputSystem();

        controller.Player.Movment.performed += ctx => move = ctx.ReadValue<Vector2>();
        controller.Player.Movment.canceled += _ => move = Vector2.zero;

        controller.Player.MouseMovment.performed += ctx => mouseMove = ctx.ReadValue<Vector2>() * Time.fixedDeltaTime * sensitivity * sensMultiplier;
        controller.Player.MouseMovment.canceled += _ => mouseMove = Vector2.zero;

        controller.Player.Jump.performed += _ => jump = true;
        controller.Player.Jump.canceled += _ => jump = false;
    }

    private void FixedUpdate()
    {
        motor.Move(move, mouseMove, jump);
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }
}
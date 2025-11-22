//Copyright(c) 2025 TechScafflod and Modular Motion Works

//Permission is hereby granted to use, modify, and integrate the assets contained in this project
//for the sole purpose of creating and distributing interactive game experiences.

//Restrictions:
//-Assets may NOT be sold, licensed, or redistributed as standalone content.
//- Assets may NOT be repackaged or marketed outside of a game project.
//- Modifications are allowed, but derivative works must also remain bound by this restriction.

//This license applies to all scripts, prefabs, scenes, and UI elements included in the project.
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; //Put cam transform here
    public CharacterController playerController; //Put character controller here
    private PlayerInput inputActions; //Put Input action map here
    private float MouseSensitivity = 1.0f; //Mouse sensitivity
    private float Xrotation = 0f;

    private Vector3 Velocity;
    private float Gravity = -9.81f;
    [SerializeField] private Transform checkSphere;
    private float checkradius = 0.2f;
    [SerializeField] private LayerMask ground;
    bool isGrounded;
    private float speed;
    private float normalSpeed = 4.5f;
    private float sprintSpeed = 7.0f;
      
    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        inputActions = new PlayerInput();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Sprint.performed += Sprint;
        inputActions.Player.Sprint.canceled += Sprint;

        Cursor.lockState = CursorLockMode.Locked;
        speed = normalSpeed;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(checkSphere.position, checkradius, ground);

        if(isGrounded && Velocity.y < 0)
        {
            Velocity.y = -1f;
        }
        else
        {
            Velocity.y += Gravity * Time.deltaTime;
        }
        playerController.Move(Velocity * Time.deltaTime);

        Rotation();
        Move();
    }

    private void Move()
    {
        Vector2 move = inputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movemanager = transform.TransformDirection(new Vector3(move.x, 0, move.y));
        playerController.Move(movemanager * speed * Time.deltaTime );
    }
    private void Rotation()
    {
        Vector2 look = inputActions.Player.Look.ReadValue<Vector2>();

        Xrotation -= look.y;
        Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);

        transform.Rotate(Vector3.up * look.x * MouseSensitivity);
        cameraTransform.localEulerAngles = new Vector3(Xrotation, 0, 0);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Velocity.y = Mathf.Sqrt(2 * Mathf.Abs(Gravity) * 1.5f);
        }
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (isGrounded && ctx.performed)
        {
            speed = sprintSpeed;
        }
        else if (isGrounded && ctx.canceled)
        {
            speed = normalSpeed;
        }
    }
    
}

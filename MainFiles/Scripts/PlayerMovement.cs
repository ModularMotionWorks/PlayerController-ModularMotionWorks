// PlayerMovement.cs
// Created by ModularMotionWorks © 2025
// Licensed for use under custom EULA
// Version: 1.0.0
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController; //Access for the Charater Controller
    [SerializeField] Transform CheckSphere; //Check Sphere transform acccess for the gravity
    [SerializeField] LayerMask Surface; //Layers for checking of the gravity
    bool isGrounded; //Gorund check boolean  
    private float CheckRadius = 0.4f; // Check Sphere's radius
    private float Gravity = -9.81f; //Gravity value 
    private Vector3 Velocity; //velocity for push for downwards 

    private float Speed; // Speed for player
    private float walkSpeed = 6.0f;  
    private float Jumpheight = 2f;
    private float sprintSpeed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<CharacterController>(); //Reference to the character Controller
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(CheckSphere.position, CheckRadius, Surface); //Ground check

        //Logic for the gravity : - 
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -1; //Small force to keep player down
        }
        else
        {
            Velocity.y += Gravity * Time.deltaTime; //When the player is in the air gravity push down it back
        }
        playerController.Move(Velocity * Time.deltaTime); // Gravity Implementation

        //Jump Logic with check of ground and input key 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Velocity.y = Mathf.Sqrt(Jumpheight * -2f * Gravity);
        }

        Move(); // calling the move method
    }
    void Move()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); //Boolean to check the player is sprinting or not or key is accessed or not
        Speed = (isSprinting ? sprintSpeed : walkSpeed); // By ternary operators here is if else logic

        float HorizontalAxis = Input.GetAxis("Horizontal")* Speed * Time.deltaTime; // getting the input from horizontal access 
        float VerticalAxis = Input.GetAxis("Vertical")* Speed * Time.deltaTime; // Getting the input from the vertical access

        Vector3 PlayerControls = transform.right * HorizontalAxis + transform.forward * VerticalAxis; //taking the controlls in the form of vector 3 and direction of the axis input
        playerController.Move(PlayerControls); //Moving the player by the inputs of vector3 final step for movement
    }
}

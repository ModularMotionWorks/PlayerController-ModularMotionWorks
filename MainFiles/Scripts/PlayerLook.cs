// PlayerMovement.cs
// Created by ModularMotionWorks © 2025
// Licensed for use under custom EULA
// Version: 1.0.0
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1000.0f; // Sensitivity of mouse can adjust by user inputs

    private float Xrotation = 0f; // X rotation 

    public Transform Playertransform; //Accessing the Player's Transform
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Cursor lock on to the game display
    }
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //Input based on MouseX caputured here
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //Input based on MouseYs caputured here

        Xrotation -= MouseY; // Subtrating the Xrotation from MouseY input to get movement of Xrotation
        Xrotation = Mathf.Clamp(Xrotation , -90.0f , 30.0f); // Clamping camera to -90 to 30 Clamp can also done as per need of user 

        transform.localRotation = Quaternion.Euler(Xrotation , 0f ,0f); //Moving the camera based on Input of Xrotation Up and down Basically
        Playertransform.Rotate(Vector3.up * MouseX); //Playertransform to move player as per mouse moves in right or left so the player faces that direction
    }
}

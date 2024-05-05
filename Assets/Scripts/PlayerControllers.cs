using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header ("References")]
    public Camera playerCamera;

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float speedMultiplierPerJump = 1.1f; // Factor de multiplicación de velocidad por salto

    [Header("Rotation")]
    public float rotationSensibility = 100f;

    [Header("Jump")]
    public float jumpHeight = 1.9f;
    public float jumpCooldown = 0.1f; // Tiempo de cooldown entre saltos

    private float cameraVerticalAngle;
    private float lastJumpTime; // Tiempo del último salto
    private int consecutiveJumps = 0;
    private float currentWalkSpeed;
    private float currentRunSpeed;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationinput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        currentWalkSpeed = walkSpeed;
        currentRunSpeed = runSpeed;
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * currentRunSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * currentWalkSpeed;
            }
            
            if (Input.GetButton("Jump") && Time.time > lastJumpTime + jumpCooldown)
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
                lastJumpTime = Time.time;
                consecutiveJumps++;
                // Multiplica las velocidades por el factor de multiplicación
                currentWalkSpeed *= speedMultiplierPerJump;
                currentRunSpeed *= speedMultiplierPerJump;
            }
            else
            {
                consecutiveJumps = 0;
                // Reinicia las velocidades actuales a las velocidades iniciales
                currentWalkSpeed = walkSpeed;
                currentRunSpeed = runSpeed;
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        rotationinput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationinput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle + rotationinput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationinput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }
}

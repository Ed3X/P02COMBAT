using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script requiere un CharacterController adjunto al mismo GameObject
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header ("Referencias")]
    public Camera playerCamera; // Referencia a la cámara del jugador

    [Header("General")]
    public float gravityScale = -20f; // Escala de gravedad

    [Header("Movimiento")]
    public float walkSpeed = 5f; // Velocidad de caminar
    public float runSpeed = 10f; // Velocidad de correr
    public float speedMultiplierPerJump = 1.1f; // Factor de multiplicación de velocidad por salto
    public float maxSpeedMultiplier = 2.0f; // Límite para el multiplicador de velocidad al saltar

    [Header("Rotación")]
    public float rotationSensibility = 100f; // Sensibilidad de rotación

    [Header("Salto")]
    public float jumpHeight = 1.9f; // Altura del salto
    public float jumpCooldown = 0.1f; // Tiempo de cooldown entre saltos

    // Variables privadas para el control del movimiento y la rotación
    private float cameraVerticalAngle;
    private float lastJumpTime; // Tiempo del último salto
    private int consecutiveJumps = 0; // Contador de saltos consecutivos
    private float currentWalkSpeed; // Velocidad actual de caminar
    private float currentRunSpeed; // Velocidad actual de correr
    private Vector3 moveInput = Vector3.zero; // Entrada de movimiento
    private Vector3 rotationinput = Vector3.zero; // Entrada de rotación
    private CharacterController characterController; // Referencia al CharacterController

    private void Awake()
    {
        // Obtenemos el componente CharacterController adjunto al GameObject
        characterController = GetComponent<CharacterController>();
        // Asignamos las velocidades actuales a las velocidades de caminar y correr
        currentWalkSpeed = walkSpeed;
        currentRunSpeed = runSpeed;
    }

    private void Update()
    {
        // Llamamos a los métodos de rotación y movimiento en cada frame de la ejecución del juego
        Look();
        Move();
    }

    private void Move()
    {
        // Si el jugador está en el suelo
        if (characterController.isGrounded)
        {
            // Obtenemos la entrada de movimiento en los ejes horizontal y vertical
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            // Si se está presionando el botón de correr, ajustamos la velocidad de movimiento
            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * currentRunSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * currentWalkSpeed;
            }
            
            // Si se presiona el botón de salto y ha pasado el tiempo de cooldown desde el último salto
            if (Input.GetButton("Jump") && Time.time > lastJumpTime + jumpCooldown)
            {
                // Calculamos la velocidad de salto y realizamos el salto
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
                lastJumpTime = Time.time;
                consecutiveJumps++; // Aumentamos el contador de saltos consecutivos
                // Ajustamos las velocidades de caminar y correr por el factor de multiplicación, asegurándonos de que no exceda el límite
                currentWalkSpeed *= speedMultiplierPerJump;
                currentRunSpeed *= speedMultiplierPerJump;
                currentWalkSpeed = Mathf.Min(currentWalkSpeed, walkSpeed * maxSpeedMultiplier);
                currentRunSpeed = Mathf.Min(currentRunSpeed, runSpeed * maxSpeedMultiplier);
            }
            else
            {
                consecutiveJumps = 0; // Reiniciamos el contador de saltos consecutivos
                // Restablecemos las velocidades actuales a las velocidades iniciales
                currentWalkSpeed = walkSpeed;
                currentRunSpeed = runSpeed;
            }
        }

        // Aplicamos la gravedad al movimiento
        moveInput.y += gravityScale * Time.deltaTime;
        // Movemos al jugador
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        // Obtenemos la entrada de rotación en los ejes horizontal y vertical
        rotationinput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationinput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        // Ajustamos el ángulo vertical de la cámara y lo limitamos dentro de un rango específico
        cameraVerticalAngle = cameraVerticalAngle + rotationinput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        // Rotamos el jugador horizontalmente y la cámara verticalmente
        transform.Rotate(Vector3.up * rotationinput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }
}

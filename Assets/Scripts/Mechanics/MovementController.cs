using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Transform Camera;
    public CharacterController CharacterController;
    public PlayerInput Input;
    public float movespeed;
    private InputAction _move;
    private InputAction _look;
    public float Sensivity;
    private InputAction _jump;
    public float jumpforce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.01f;

    private bool _isGrounded;
    private float _yRotation;
    private Vector3 _velocity;
    public float Gravity;

    // Для звука шагов
    public AudioSource FootstepAudioSource;

    private bool _isMoving;

    void Start()
    {
        _move = Input.actions["Move"];
        _jump = Input.actions["Jump"];
        _jump.performed += _jump_performed;

        _look = Input.actions["Look"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
        HandleFootstepSound();
    }

    private void HandleLook()
    {
        var LookInputX = _look.ReadValue<Vector2>().x * Sensivity * Time.deltaTime;
        var LookInputY = -_look.ReadValue<Vector2>().y * Sensivity * Time.deltaTime;

        _yRotation += LookInputY;
        _yRotation = Mathf.Clamp(_yRotation, -60, 90);
        Camera.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);

        transform.Rotate(0, LookInputX, 0);
    }

    private void HandleMovement()
    {
        _velocity.y += Gravity * Time.deltaTime;

        var MoveInput = _move.ReadValue<Vector2>() * movespeed * Time.deltaTime;
        var MoveDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
        MoveDirection = CharacterController.transform.TransformDirection(MoveDirection);

        if (CharacterController.isGrounded)
        {
            if (_jump.triggered)
            {
                _velocity.y = jumpforce;
            }
        }

        CharacterController.Move(MoveDirection);
        CharacterController.Move(_velocity);

        // Определяем, движется ли персонаж
        _isMoving = CharacterController.isGrounded && MoveInput.magnitude > 0;
    }

    private void HandleFootstepSound()
    {
        if (_isMoving)
        {
            if (!FootstepAudioSource.isPlaying)
            {
                FootstepAudioSource.Play(); // Начать воспроизведение, если звук не играет
            }
        }
        else
        {
            if (FootstepAudioSource.isPlaying)
            {
                FootstepAudioSource.Stop(); // Остановить воспроизведение, если персонаж остановился
            }
        }
    }

    private void _jump_performed(InputAction.CallbackContext obj)
    {
        // Логика прыжка, если потребуется
    }
}
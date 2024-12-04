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
    public float groundCheckRadius = 1000;

    
    private bool _isGrounded;
    private float _yRotation;
    private Vector3 _velocity;
    public float Gravity;
    

    
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
        var LookInputX = _look.ReadValue<Vector2>().x * Sensivity * Time.deltaTime;
        var LookInputY = -_look.ReadValue<Vector2>().y * Sensivity * Time.deltaTime;

        _yRotation += LookInputY;
        _yRotation = Mathf.Clamp(_yRotation, -60, 90);
        Camera.localRotation = Quaternion.Euler(_yRotation, 0, 0);

        transform.Rotate(0, LookInputX, 0);

        _velocity.y += Gravity * Time.deltaTime;

        var MoveInput = _move.ReadValue<Vector2>() * movespeed * Time.deltaTime;
        var MoveDirection = new Vector3(MoveInput.x, _velocity.y, MoveInput.y);
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










    }
    private void _jump_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
    
}

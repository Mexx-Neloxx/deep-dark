using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCOntroller : MonoBehaviour
{

    public PlayerInput Input;
    
    public float RotationSpeed;

    private InputAction _look;

    public Transform Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _look = Input.actions["Look"];

    }

    // Update is called once per frame
    void Update()
    {
        var lookInput = _look.ReadValue<Vector2>();

        var lookX = lookInput.x * RotationSpeed * Time.deltaTime;
        var lookY = -lookInput.y * RotationSpeed * Time.deltaTime;


        transform.RotateAround(Target.position, Vector3.up, lookX);
        transform.RotateAround(Target.position, transform.right, lookY);

    }
}

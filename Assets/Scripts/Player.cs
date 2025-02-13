using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private float _moveSpeed = 5f;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontalInput = _movementJoystick.Horizontal;
        float verticalInput = _movementJoystick.Vertical;
        Vector3 movementDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        _characterController.Move(movementDirection * _moveSpeed * Time.deltaTime);
    }
}

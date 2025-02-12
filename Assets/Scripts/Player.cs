using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick movementJoystick;  // ссылка на компонент джойстика
    public float moveSpeed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Получаем вход с джойстика
        float horizontal = movementJoystick.Horizontal();
        float vertical = movementJoystick.Vertical();
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

}

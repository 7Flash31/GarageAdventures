using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLook : MonoBehaviour
{
    public float lookSpeed = 0.2f;
    private Vector2 lastTouchPosition;
    private bool isLooking = false;

    // Для горизонтального поворота игрока
    public Transform playerBody;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;



            foreach(Touch touch in Input.touches)
            {
                // Если касание над UI, пропускаем его

                if(touch.phase == TouchPhase.Began)
                {
                    lastTouchPosition = touch.position;
                    isLooking = true;
                }
                else if(touch.phase == TouchPhase.Moved && isLooking)
                {
                    Vector2 delta = touch.position - lastTouchPosition;
                    lastTouchPosition = touch.position;

                    // Поворот игрока по горизонтали
                    float horizontalRotation = delta.x * lookSpeed;
                    playerBody.Rotate(0f, horizontalRotation, 0f);

                    // Поворот камеры по вертикали (ограничим угол от -80 до 80 градусов)
                    float verticalRotation = -delta.y * lookSpeed;
                    float currentX = transform.localEulerAngles.x;
                    currentX = (currentX > 180) ? currentX - 360 : currentX;
                    float newX = Mathf.Clamp(currentX + verticalRotation, -80f, 80f);
                    transform.localEulerAngles = new Vector3(newX, 0f, 0f);
                }
                else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isLooking = false;
                }
            }
        }
    }
}

using UnityEngine;

public class TouchLook : MonoBehaviour
{
    [SerializeField] private float _lookSpeed = 0.2f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private RectTransform _allowedLookArea;

    private Vector2 _lastTouchPosition;
    private bool _isLooking;

    private void Update()
    {
        if(Input.touchCount <= 0)
        {
            return;
        }

        foreach(Touch touch in Input.touches)
        {
            if(_allowedLookArea != null && !RectTransformUtility.RectangleContainsScreenPoint(_allowedLookArea, touch.position, null))
            {
                continue;
            }

            if(touch.phase == TouchPhase.Began)
            {
                _lastTouchPosition = touch.position;
                _isLooking = true;
            }
            else if(touch.phase == TouchPhase.Moved && _isLooking)
            {
                Vector2 delta = touch.position - _lastTouchPosition;
                _lastTouchPosition = touch.position;

                float horizontalRotation = delta.x * _lookSpeed;
                _playerBody.Rotate(0f, horizontalRotation, 0f);

                float verticalRotation = -delta.y * _lookSpeed;
                float currentX = transform.localEulerAngles.x;
                currentX = (currentX > 180f) ? currentX - 360f : currentX;
                float newX = Mathf.Clamp(currentX + verticalRotation, -80f, 80f);
                transform.localEulerAngles = new Vector3(newX, 0f, 0f);
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isLooking = false;
            }
        }
    }
}

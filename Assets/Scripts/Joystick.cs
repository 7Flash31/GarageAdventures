using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;

    private Vector2 _inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventData.pressEventCamera, out Vector2 localPosition))
        {
            localPosition.x /= _background.sizeDelta.x;
            localPosition.y /= _background.sizeDelta.y;

            _inputVector = new Vector2(localPosition.x * 2f, localPosition.y * 2f);
            if(_inputVector.magnitude > 1f)
            {
                _inputVector.Normalize();
            }

            _handle.anchoredPosition = new Vector2(
                _inputVector.x * (_background.sizeDelta.x / 3f),
                _inputVector.y * (_background.sizeDelta.y / 3f));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal => _inputVector.x;

    public float Vertical => _inputVector.y;
}

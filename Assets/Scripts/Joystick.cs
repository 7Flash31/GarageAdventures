using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Ссылки на RectTransform для фона и ручки джойстика
    public RectTransform background;
    public RectTransform handle;

    // Вектор ввода, нормализованный до длины 1 (если больше – нормализуем)
    private Vector2 inputVector;

    // Обработка события касания (нажатия)
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // Обработка перетаскивания
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        // Преобразуем позицию касания из экранных координат в локальные координаты фона
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
        {
            // Нормализуем позицию в диапазоне от -1 до 1
            pos.x = pos.x / background.sizeDelta.x;
            pos.y = pos.y / background.sizeDelta.y;

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            // Если вектор ввода больше единицы, нормализуем его
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Перемещаем ручку джойстика: умножаем на часть размера фона для ограничения движения
            handle.anchoredPosition = new Vector2(inputVector.x * (background.sizeDelta.x / 3),
                                                    inputVector.y * (background.sizeDelta.y / 3));
        }
    }

    // Обработка отпускания касания
    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    // Методы для получения горизонтального и вертикального ввода
    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}

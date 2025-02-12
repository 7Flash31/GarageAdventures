using UnityEngine;

public class PickupManager : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0)
        {
            // Берём первое касание (можно обработать мульти-тач, если нужно)
            Touch touch = Input.GetTouch(0);

            // Реагируем на начало касания (или можно использовать Phase.Ended)
            if(touch.phase == TouchPhase.Began)
            {
                // Создаем луч из экрана, используя позицию касания
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Если луч пересекается с каким-либо коллайдером
                if(Physics.Raycast(ray, out hit))
                {
                    // Пытаемся получить нужный компонент у объекта, с которым столкнулся луч
                    Item item = hit.collider.GetComponent<Item>();

                    if(item != null)
                    {
                        // Компонент найден — выполняем нужные действия
                        Debug.Log("Найден компонент MyComponent на объекте: " + hit.collider.gameObject.name);
                    }
                    else
                    {
                        Debug.Log("Компонент MyComponent не найден на объекте: " + hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}

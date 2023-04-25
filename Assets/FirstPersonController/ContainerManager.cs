using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContainerManager : MonoBehaviour, IPointerEnterHandler,
                                IPointerExitHandler, IPointerDownHandler
{
    // Объект FPC для вытаскивания функции из скрипта
    public GameObject CharacterController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Триггер наведения на объект
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            // Из имени объекта "Text0" обрезаем "Text" и преобразуем в int
            // В итоге получаем индекс контейнера
            // Объект "Container0" лежит ПОД "Text0", на него триггер не регает
            int containerId = int.Parse(gameObject.name.Substring(4));
            CharacterController.GetComponent<InventoryManager>().OutDescription(containerId);
        }
    }

    // Триггер прекращения наведения на объект
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            CharacterController.GetComponent<InventoryManager>().ClearDescription();
        }
    }

    // Триггер клика по объекту
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            int containerId = int.Parse(gameObject.name.Substring(4));
            CharacterController.GetComponent<InventoryManager>().UseObject(containerId);
        }
    }
}

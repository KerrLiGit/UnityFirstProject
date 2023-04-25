using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContainerManager : MonoBehaviour, IPointerEnterHandler,
                                IPointerExitHandler, IPointerDownHandler
{
    // ������ FPC ��� ������������ ������� �� �������
    public GameObject CharacterController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ������� ��������� �� ������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            // �� ����� ������� "Text0" �������� "Text" � ����������� � int
            // � ����� �������� ������ ����������
            // ������ "Container0" ����� ��� "Text0", �� ���� ������� �� ������
            int containerId = int.Parse(gameObject.name.Substring(4));
            CharacterController.GetComponent<InventoryManager>().OutDescription(containerId);
        }
    }

    // ������� ����������� ��������� �� ������
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            CharacterController.GetComponent<InventoryManager>().ClearDescription();
        }
    }

    // ������� ����� �� �������
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Substring(0, 4) == "Text")
        {
            int containerId = int.Parse(gameObject.name.Substring(4));
            CharacterController.GetComponent<InventoryManager>().UseObject(containerId);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    // Панель инвентаря
    public GameObject InventoryPanel;

    // Количество типов элементов в инвентаре
    private int arrayLen;

    // Массив имен объектов в инвентаре
    private string[] objectName;

    // Массив описаний объектов в инвентаре
    private string[] objectDescription;

    // Массив внешнего вида объектов для отображения в контейнере
    public Image[] objectItem;

    // Массив, содержащий текстовые поля для отображения количества объектов
    public Text[] textCnt;

    // Массив количества предметов каждого типа в инвентаре
    private int[] cnt;

    // Выбранный объект
    public GameObject ChosenObject;

    // Пустое изображение для пустых ячеек иныентаря
    public Sprite blankImage;

    // Камера для определения луча
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        arrayLen = 16;
        objectName = new string[arrayLen];
        objectDescription = new string[arrayLen];
        cnt = new int[arrayLen];
        foreach (Image image in objectItem)
        {
            image.sprite = blankImage;
        }
        ChosenObject.transform.Find("IconImage").GetComponent<Image>().sprite = blankImage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Получение объекта, на который направлена камера
    private GameObject GetHitObject()
    {
        // Сам луч
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // Запись объекта, в который пришел луч, в переменную
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.collider.gameObject;
    }

    // Подбор предмета, если СС подошел близко и нажал F, а также если луч камеры направлен на предмет
    private void OnTriggerStay(Collider other)
    {
        string tempObjectName = other.transform.Find("Canvas").transform.Find("Name").GetComponent<Text>().text;
        Image tempObjectImage = other.transform.Find("Canvas").transform.Find("IconImage").GetComponent<Image>();
        string tempObjectDescription = other.transform.Find("Canvas").transform.Find("Description").GetComponent<Text>().text;

        GameObject hitObject = GetHitObject();

        if (Input.GetKeyDown(KeyCode.F) && hitObject.tag == "Item" && 
            hitObject.transform.Find("Canvas").transform.Find("Name").GetComponent<Text>().text == tempObjectName)
        {
            int i = 0;
            bool flag = false;
            i = 0;
            for (; i < arrayLen; i++)
            {
                if (this.objectName[i] == null)
                {
                    break;
                }
                if (this.objectName[i] == tempObjectName)
                {
                    cnt[i]++;
                    textCnt[i].text = cnt[i].ToString();
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                objectName[i] = tempObjectName;
                objectDescription[i] = tempObjectDescription;
                objectItem[i].sprite = tempObjectImage.sprite;
                cnt[i] = 1;
                textCnt[i].text = cnt[i].ToString();
            }
            Destroy(other.gameObject);
        }
    }

    public void GetItem(GameObject item)
    {
        Destroy(item);
    }

    // Вывод описания при наведении на объект
    // Вызывается из ContainerManager
    public void OutDescription(int containerId)
    {
        ChosenObject.transform.Find("IconImage").GetComponent<Image>().sprite = objectItem[containerId].sprite;
        ChosenObject.transform.Find("Name").GetComponent<Text>().text = objectName[containerId];
        ChosenObject.transform.Find("Description").GetComponent<Text>().text = objectDescription[containerId];
    }

    // Очистка описания при прекращении наведения на объект
    // Вызывается из ContainerManager
    public void ClearDescription()
    {
        ChosenObject.transform.Find("IconImage").GetComponent<Image>().sprite = blankImage;
        ChosenObject.transform.Find("Name").GetComponent<Text>().text = "";
        ChosenObject.transform.Find("Description").GetComponent<Text>().text = "";
    }

    // Использования объекта при клике на объект
    // Вызывается из ContainerManager
    public void UseObject(int containerId)
    {
        //if (container[containerId] == "Сыр" || container[containerId] == "Колбаса")
    }
}

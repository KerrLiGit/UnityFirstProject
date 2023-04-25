using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    // ������ ���������
    public GameObject InventoryPanel;

    // ���������� ����� ��������� � ���������
    private int arrayLen;

    // ������ ���� �������� � ���������
    private string[] objectName;

    // ������ �������� �������� � ���������
    private string[] objectDescription;

    // ������ �������� ���� �������� ��� ����������� � ����������
    public Image[] objectItem;

    // ������, ���������� ��������� ���� ��� ����������� ���������� ��������
    public Text[] textCnt;

    // ������ ���������� ��������� ������� ���� � ���������
    private int[] cnt;

    // ��������� ������
    public GameObject ChosenObject;

    // ������ ����������� ��� ������ ����� ���������
    public Sprite blankImage;

    // ������ ��� ����������� ����
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

    // ��������� �������, �� ������� ���������� ������
    private GameObject GetHitObject()
    {
        // ��� ���
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // ������ �������, � ������� ������ ���, � ����������
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.collider.gameObject;
    }

    // ������ ��������, ���� �� ������� ������ � ����� F, � ����� ���� ��� ������ ��������� �� �������
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

    // ����� �������� ��� ��������� �� ������
    // ���������� �� ContainerManager
    public void OutDescription(int containerId)
    {
        ChosenObject.transform.Find("IconImage").GetComponent<Image>().sprite = objectItem[containerId].sprite;
        ChosenObject.transform.Find("Name").GetComponent<Text>().text = objectName[containerId];
        ChosenObject.transform.Find("Description").GetComponent<Text>().text = objectDescription[containerId];
    }

    // ������� �������� ��� ����������� ��������� �� ������
    // ���������� �� ContainerManager
    public void ClearDescription()
    {
        ChosenObject.transform.Find("IconImage").GetComponent<Image>().sprite = blankImage;
        ChosenObject.transform.Find("Name").GetComponent<Text>().text = "";
        ChosenObject.transform.Find("Description").GetComponent<Text>().text = "";
    }

    // ������������� ������� ��� ����� �� ������
    // ���������� �� ContainerManager
    public void UseObject(int containerId)
    {
        //if (container[containerId] == "���" || container[containerId] == "�������")
    }
}

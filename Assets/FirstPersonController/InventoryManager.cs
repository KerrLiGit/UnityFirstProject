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

    // ������ ��������� ����� ��� ��������� �������� �������
    private Vector3[] selectPosition;

    // ������ ��������� ������
    public GameObject Selection;

    // ������ ���������� ������
    public int selectionId;

    // ��������� ������
    public GameObject ChosenObject;

    // ������ ����������� ��� ������ ����� ���������
    public Sprite blankImage;

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

        selectPosition = new Vector3[arrayLen];
         selectPosition[0] = new Vector3(-75,  75, 0);  selectPosition[1] = new Vector3(-25,  75, 0);  selectPosition[2] = new Vector3(25,  75, 0);  selectPosition[3] = new Vector3(75,  75, 0);
         selectPosition[4] = new Vector3(-75,  25, 0);  selectPosition[5] = new Vector3(-25,  25, 0);  selectPosition[6] = new Vector3(25,  25, 0);  selectPosition[7] = new Vector3(75,  25, 0);
         selectPosition[8] = new Vector3(-75, -25, 0);  selectPosition[9] = new Vector3(-25, -25, 0); selectPosition[10] = new Vector3(25, -25, 0); selectPosition[11] = new Vector3(75, -25, 0);
        selectPosition[12] = new Vector3(-75, -75, 0); selectPosition[13] = new Vector3(-25, -75, 0); selectPosition[14] = new Vector3(25, -75, 0); selectPosition[15] = new Vector3(75, -75, 0);
        Selection.SetActive(false);
        selectionId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // ��������� �������, �� ������� ���������� ������
        GameObject item = GetHitObject();

        // ������ ��������, ���� �� ������� ������ � ����� F, � ����� ���� ��� ������ ��������� �� �������
        if (Input.GetKeyDown(KeyCode.F) && item && item.tag == "Item")
        {
            string tempObjectName = item.transform.Find("Canvas").transform.Find("Name").GetComponent<Text>().text;
            Image tempObjectImage = item.transform.Find("Canvas").transform.Find("IconImage").GetComponent<Image>();
            string tempObjectDescription = item.transform.Find("Canvas").transform.Find("Description").GetComponent<Text>().text;

            if (Vector3.Distance(gameObject.transform.position, item.transform.position) < 3 &&
                item.transform.Find("Canvas").transform.Find("Name").GetComponent<Text>().text == tempObjectName)
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
                Destroy(item.gameObject);
            }
        }
    }

    // ��������� �������, �� ������� ���������� ������
    private GameObject GetHitObject()
    {
        // ��� ���
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ������ �������, � ������� ������ ���, � ����������
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
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
    public void UseItem(int containerId)
    {
        if (objectName[containerId] == "���")
        {
            Dec(containerId);
            gameObject.transform.Find("UserCanvas").Find("UserInterface").Find("HPBar").GetComponent<StatsController>().changeCurHP(5);
        }
        else if (objectName[containerId] == "�������")
        {
            Dec(containerId);
            gameObject.transform.Find("UserCanvas").Find("UserInterface").Find("HPBar").GetComponent<StatsController>().changeCurHP(5);
        }
        else if (objectName[containerId] == "��������")
        {
            if (Selection.activeSelf)
            {
                selectionId = -1;
                Selection.SetActive(false);
                gameObject.transform.Find("UserCanvas").Find("UserInterface").Find("HPBar").GetComponent<StatsController>().equipGun(0);
            }
            else
            {
                selectionId = containerId;
                Selection.SetActive(true);
                Selection.GetComponent<RectTransform>().anchoredPosition = selectPosition[selectionId];
                gameObject.transform.Find("UserCanvas").Find("UserInterface").Find("HPBar").GetComponent<StatsController>().equipGun(40);
            }
        }
    }

    // ���������� ���������� ������� �� 1 � ���������
    private void Dec(int containerId)
    {
        if (cnt[containerId] != 0)
        {
            if (cnt[containerId] != 0)
            {
                cnt[containerId]--;
                textCnt[containerId].text = cnt[containerId].ToString();
            }
            if (cnt[containerId] == 0)
            {
                // �������� ������ �� ������� ���� ��������� ��������� � ���������
                // (���� �������� 0 ��������� �������� ����)
                int i;
                for (i = containerId + 1; i < arrayLen; i++)
                {
                    objectName[i - 1] = objectName[i];
                    objectDescription[i - 1] = objectDescription[i];
                    objectItem[i - 1].sprite = objectItem[i].sprite;
                    cnt[i - 1] = cnt[i];
                    textCnt[i - 1].text = textCnt[i].text;
                }
                objectName[i - 1] = "";
                objectDescription[i - 1] = "";
                objectItem[i - 1].sprite = blankImage;
                cnt[i - 1] = 0;
                textCnt[i - 1].text = "";

                ClearDescription();

                // �������� ����������� �������, ���� �����
                if (Selection.activeSelf && selectionId > containerId)
                {
                    selectionId--;
                    Selection.GetComponent<RectTransform>().anchoredPosition = selectPosition[selectionId];
                }
            }
        }
    }
}

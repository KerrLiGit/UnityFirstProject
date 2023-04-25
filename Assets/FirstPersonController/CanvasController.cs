using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    // ������, ���������� ��� ������ ����������������� ����������
    private Canvas UserCanvas;

    // ������ ����
    private GameObject GameMenu;

    // ������ ����������������� ����������
    private GameObject UserInterface;

    // ������ ���������
    private GameObject Inventory;

    // Start is called before the first frame update
    void Start()
    {
        UserCanvas = GetComponent<Canvas>();

        GameMenu = UserCanvas.transform.Find("GameMenu").gameObject;
        GameMenu.SetActive(false);

        UserInterface = UserCanvas.transform.Find("UserInterface").gameObject;
        UserInterface.SetActive(true);

        Inventory = UserCanvas.transform.Find("Inventory").gameObject;
        Inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ��� ������� Escape ��������� � ��������� ������� ����, 
        // ������ ��� ���� �������������� ��� ������������� ����� ��� ������ �� ����
        if (Input.GetKeyDown(KeyCode.Escape) && !GameMenu.activeSelf)
        {
            UserInterface.SetActive(false);
            GameMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Inventory.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameMenu.activeSelf)
        {
            UserInterface.SetActive(true);
            GameMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        // ��� ������� I ��������� � ��������� ���������
        if (Input.GetKeyDown(KeyCode.I) && !GameMenu.activeSelf && !Inventory.activeSelf)
        {
            Inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !GameMenu.activeSelf && Inventory.activeSelf)
        {
            Inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

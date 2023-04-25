using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // ������ � ����������� �������� ����
    private Canvas MainMenuCanvas;

    // ������ �������� ����, �������������� ��� ������ ����
    private GameObject Menu;

    // ������ � ����������� � ������� ����
    private GameObject Options;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuCanvas = GetComponent<Canvas>();

        Menu = MainMenuCanvas.transform.Find("Menu").gameObject;
        Menu.SetActive(true);

        Options = MainMenuCanvas.transform.Find("Options").gameObject;
        Options.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScene()
    {
        SceneManager.LoadScene("FirstPersonScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

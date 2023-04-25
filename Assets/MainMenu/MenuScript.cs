using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Объект с интерфейсом главного меню
    private Canvas MainMenuCanvas;

    // Панель главного меню, запускающегося при старте игры
    private GameObject Menu;

    // Панель с настройками в главном меню
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

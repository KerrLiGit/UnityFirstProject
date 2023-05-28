using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    // Объект, содержащий все панели пользовательского интерфейса
    private Canvas UserCanvas;

    // Панель меню
    private GameObject GameMenu;

    // Панель пользовательского интерфейса
    private GameObject UserInterface;

    // Панель инвентаря
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
        // При нажатии Escape открывать и закрывать игровое меню, 
        // курсор при этом разблокируется или заблокируется назад при выходе из меню
        if (Input.GetKeyDown(KeyCode.Escape) && !GameMenu.activeSelf)
        {
            UserInterface.SetActive(false);
            GameMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Inventory.SetActive(false);

            // Полосы здоровья противников
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameMenu.activeSelf)
        {
            UserInterface.SetActive(true);
            GameMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            // Полосы здоровья противников
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(true);
            }
        }

        // При нажатии I открывать и закрывать инвентарь
        if (Input.GetKeyDown(KeyCode.I) && !GameMenu.activeSelf && !Inventory.activeSelf)
        {
            Inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            // Полосы здоровья противников
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.I) && !GameMenu.activeSelf && Inventory.activeSelf)
        {
            Inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            // Полосы здоровья противников
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(true);
            }
        }
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

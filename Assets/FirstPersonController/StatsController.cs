using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsController : MonoBehaviour
{
    // Здоровье
    public float maxHP = 100;

    // Текущее здоровье
    public float curHP = 100;

    // Слайдер для отображения здоровья
    private Slider HPBar;

    // Дамаг при стрельбе
    public float shootDamage = 40;

    // Start is called before the first frame update
    void Start()
    {
        HPBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Вычисление текущего значения HP для отображения в интерфейсе 
        HPBar.value = curHP / maxHP;

        // Стрельба по клику мыши
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Enemy = GetHitObject();
            if (Enemy != null && Enemy.tag == "Enemy")
            {
                Enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyHPController>().getDamage(shootDamage);
            }
        }
    }

    // Получение объекта, на который направлена камера
    private GameObject GetHitObject()
    {
        // Сам луч
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Запись объекта, в который пришел луч, в переменную
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

}

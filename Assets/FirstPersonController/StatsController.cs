using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsController : MonoBehaviour
{
    // Объект игрока
    public GameObject Player;

    // Здоровье
    public float maxHP = 100;

    // Текущее здоровье
    public float curHP = 100;

    // Слайдер для отображения здоровья
    private Slider HPBar;

    // Дамаг при стрельбе
    public float shootDamage = 40;

    public float enemyHPBarDistance = 30;

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

        // Получение объекта противника
        GameObject Enemy = GetHitObject();

        // Стрельба по клику мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (Enemy != null && Enemy.tag == "Enemy")
            {
                Enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().getDamage(shootDamage);
            }
        }

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // Показ полосы здоровья противника в зависимости от расстояния
            if (Vector3.Distance(Player.transform.position, enemy.transform.position) < enemyHPBarDistance &&
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().getShowHPBar())
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(true);
            }
            else
            {
                enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyStatsController>().showHPBar(false);
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

    // Изменение значения текущего НР
    // Получение урона, лечение
    public void changeCurHP(float value)
    {
        curHP += value;
        if (curHP < 0)
        {
            curHP = 0;
        }
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
    }

    // Получить урон дальнобойного оружия
    public float getGunDamage()
    {
        return shootDamage;
    }

    // Экипировать оружие с уроном damage
    public void equipGun(float damage)
    {
        shootDamage = damage;
    }

}

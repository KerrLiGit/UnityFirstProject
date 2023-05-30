using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsController : MonoBehaviour
{
    // Здоровье
    public float maxHP = 100;

    // Текущее здоровье
    public float curHP = 100;

    // Объект врага
    public GameObject Enemy;

    private Vector3 offset;

    // Переменная, отвечающая за показ/скрытие полосы здоровья врага
    public bool isShowHPBar;

    // Объект для запуска анимаций
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = Enemy.GetComponent<Animator>();
        offset = new Vector3(0, 2, 0);
        isShowHPBar = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Вывод здоровья в полосу здоровья
        GetComponent<Slider>().value = curHP / maxHP;

        // Вычисление позиции полоски здоровья на экране
        Vector3 pos = Camera.main.WorldToScreenPoint(Enemy.transform.position + offset);
        if (pos.x > -20 && pos.y > 0 && pos.z > 0 && isShowHPBar)
        {
            GetComponent<RectTransform>().position = pos;
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector3(-10, -10, -10);
        }

        // Удаление противника если нет здоровья, анимация смерти
        if (curHP <= 0)
        {
            animator.SetTrigger("DeathTrigger");
            Destroy(Enemy, 5);
        }
    }

    // Получение value единиц урона
    // Вызывается из StatsController
    public void getDamage(float value)
    {
        curHP -= value;
    }

    // Показать/скрыть полосу здоровья противника
    // Вызывается из StatsController
    public void showHPBar(bool value)
    {
        isShowHPBar = value;
    }

    // Состояние полосы здоровья противника (разрешено ли показывать полосу)
    // Вызывается из StatsController
    public bool getShowHPBar()
    {
        return isShowHPBar;
    }

    // Атака гг на value единиц урона
    public void attack()
    {
        animator.SetTrigger("AttackTrigger");
    }

}

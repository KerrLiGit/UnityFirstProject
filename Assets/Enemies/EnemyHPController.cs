using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPController : MonoBehaviour
{
    // Здоровье
    public float maxHP = 100;

    // Текущее здоровье
    public float curHP = 100;

    // Объект врага
    public GameObject Enemy;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(Enemy.transform.position + offset);
        if (pos.x > -20 && pos.y > 0 && pos.z > 0)
        {
            GetComponent<RectTransform>().position = pos;
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector3(-10, -10, -10);
        }
        GetComponent<Slider>().value = curHP / maxHP;
    }

    // Получение value единиц урона
    // Вызывается из StatsController
    public void getDamage(float value)
    {
        curHP -= value;
    }

}

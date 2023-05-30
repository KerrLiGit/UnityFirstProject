using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsController : MonoBehaviour
{
    // ��������
    public float maxHP = 100;

    // ������� ��������
    public float curHP = 100;

    // ������ �����
    public GameObject Enemy;

    private Vector3 offset;

    // ����������, ���������� �� �����/������� ������ �������� �����
    public bool isShowHPBar;

    // ������ ��� ������� ��������
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
        // ����� �������� � ������ ��������
        GetComponent<Slider>().value = curHP / maxHP;

        // ���������� ������� ������� �������� �� ������
        Vector3 pos = Camera.main.WorldToScreenPoint(Enemy.transform.position + offset);
        if (pos.x > -20 && pos.y > 0 && pos.z > 0 && isShowHPBar)
        {
            GetComponent<RectTransform>().position = pos;
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector3(-10, -10, -10);
        }

        // �������� ���������� ���� ��� ��������, �������� ������
        if (curHP <= 0)
        {
            animator.SetTrigger("DeathTrigger");
            Destroy(Enemy, 5);
        }
    }

    // ��������� value ������ �����
    // ���������� �� StatsController
    public void getDamage(float value)
    {
        curHP -= value;
    }

    // ��������/������ ������ �������� ����������
    // ���������� �� StatsController
    public void showHPBar(bool value)
    {
        isShowHPBar = value;
    }

    // ��������� ������ �������� ���������� (��������� �� ���������� ������)
    // ���������� �� StatsController
    public bool getShowHPBar()
    {
        return isShowHPBar;
    }

    // ����� �� �� value ������ �����
    public void attack()
    {
        animator.SetTrigger("AttackTrigger");
    }

}

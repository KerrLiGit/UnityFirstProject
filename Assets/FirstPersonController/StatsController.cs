using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsController : MonoBehaviour
{
    // ������ ������
    public GameObject Player;

    // ��������
    public float maxHP = 100;

    // ������� ��������
    public float curHP = 100;

    // ������� ��� ����������� ��������
    private Slider HPBar;

    // ����� ��� ��������
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
        // ���������� �������� �������� HP ��� ����������� � ���������� 
        HPBar.value = curHP / maxHP;

        // ��������� ������� ����������
        GameObject Enemy = GetHitObject();

        // �������� �� ����� ����
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
            // ����� ������ �������� ���������� � ����������� �� ����������
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

    // ��������� �������� �������� ��
    // ��������� �����, �������
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

    // �������� ���� ������������� ������
    public float getGunDamage()
    {
        return shootDamage;
    }

    // ����������� ������ � ������ damage
    public void equipGun(float damage)
    {
        shootDamage = damage;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsController : MonoBehaviour
{
    // ��������
    public float maxHP = 100;

    // ������� ��������
    public float curHP = 100;

    // ������� ��� ����������� ��������
    private Slider HPBar;

    // ����� ��� ��������
    public float shootDamage = 40;

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

        // �������� �� ����� ����
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Enemy = GetHitObject();
            if (Enemy != null && Enemy.tag == "Enemy")
            {
                Enemy.transform.Find("Canvas").transform.Find("HPBarEnemy").GetComponent<EnemyHPController>().getDamage(shootDamage);
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

}

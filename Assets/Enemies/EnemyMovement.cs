using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Объект для передвижения по сетке навигации
    private NavMeshAgent navMeshAgent;

    // Объект игрока
    private GameObject Player;

    // Объект для запуска анимаций
    private Animator animator;

    // Расстояние, на котором начинается преследование игрока
    public float Distance = 10;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Player = (GameObject.FindGameObjectsWithTag("Player"))[0];
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Расстояние от игрока до противника
        float dist = Vector3.Distance(Player.transform.position, transform.position);

        if (dist >= Distance)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("Run", false);
        }
        else
        {
            navMeshAgent.SetDestination(Player.transform.position);
            animator.SetBool("Run", true);
        }       
    }
}

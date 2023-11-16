using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UIElements;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }
    #region Variables

    [Header("Enemy Data")]
    private float maxHP;
    private float currentHP;
    [SerializeField] private int score;

    [Header("Movement")]
    private float attackRange;
    private float viewRange;
    private bool alwaysFollow;

    private NavMeshAgent navMeshAgent;

    private PlayerController target;
    private WeaponController weaponController;
    private Renderer enemyRenderer;

    [SerializeField] private Transform[] points;
    private int destPoint = 0;


    #endregion

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRenderer =GetComponentInChildren<Renderer>();
        weaponController = GetComponent<WeaponController>();

        navMeshAgent.autoBraking = false;

        //GoToNextDestination();
        //maxHP = currentHP = EnemyData.MaxLife;
        alwaysFollow = EnemyData.AlwaysFollow;
        viewRange = EnemyData.ViewRange;
        attackRange = EnemyData.AttackRange;
        enemyRenderer.material = EnemyData.EnemyMaterial;

        navMeshAgent.speed = EnemyData.Speed;
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
            Destroy(this.gameObject);
    
            
    }

    private void GoToNextDestination()
    {
        

        if (points.Length == 0)
            return;

        navMeshAgent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;

        

    }

    private void Update()
    {
        //search player using a raycast
        SearchEnemy();
        
        
        //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 0.1f)
        //{
        //    GoToNextDestination();
        //    navMeshAgent.autoBraking = false;
        //    navMeshAgent.stoppingDistance = 0.5f;
        //}
    }

    private void SearchEnemy()
    {
        NavMeshHit hit;

        if (!navMeshAgent.Raycast(target.transform.position, out hit))
        {
            if (hit.distance <= 10)
            {
                navMeshAgent.SetDestination(target.transform.position);
                navMeshAgent.stoppingDistance = 5f;
                navMeshAgent.autoBraking = true;
                transform.LookAt(target.transform.position);

                if (hit.distance <= 7)
                {
                    if (weaponController.CanShoot())
                        weaponController.Shoot();
                }
                
            }
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.UpdateScore(score);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/Data", order = 0)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private string description;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float maxLife;
    [SerializeField] private float viewRange;
    [SerializeField] private float attackRange;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private bool alwaysFollow;


    [SerializeField] private int spawnerWeight;

    public float Speed { get => speed; }
    public float FireRate { get => fireRate; }
    public float MaxLife { get => maxLife; }
    public Material EnemyMaterial { get => enemyMaterial; }
    public float ViewRange { get => viewRange; }
    public float AttackRange { get => attackRange; }
    public bool AlwaysFollow { get => alwaysFollow; }
    public int SpawnerWeight { get => spawnerWeight; }
}

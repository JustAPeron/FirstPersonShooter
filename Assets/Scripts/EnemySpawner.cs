using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
[SerializeField] private int enemyNumber;
[SerializeField] private int spawnNumber;
[SerializeField] private GameObject enemyObject;
[SerializeField] private EnemyData[] enemyDatas;

private Coroutine spawnCoroutine;

private int totalWeights;


private void Start()
{
    for (int i = 0; i < enemyDatas.Length; i++)
    {
        totalWeights += enemyDatas[i].SpawnerWeight;
    }

}
private void Update()
{
    if (Input.GetKeyDown(KeyCode.E)&& spawnCoroutine == null)
    {
        Debug.Log("E pressed");
        spawnCoroutine = StartCoroutine(Spawn());
    }
}

IEnumerator Spawn()
{
    InstantiateEnemy();
    Debug.Log("Attempt");
    yield return new WaitForSeconds(1.5f);
    if (GameObject.FindGameObjectsWithTag("Enemy").Length >= enemyNumber)
    {
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < enemyNumber);
        spawnCoroutine = StartCoroutine(Spawn());
    }
    else
    {
        spawnCoroutine = StartCoroutine(Spawn());
    }
}

private void InstantiateEnemy()
{
    float positionX = Random.Range(transform.position.x - 15, transform.position.x +15);
    float positionZ = Random.Range(transform.position.z - 15, transform.position.z +15);
    Vector3 spawnPoint= new Vector3(positionX, transform.position.y, positionZ);
    GameObject currentEnemy = Instantiate(enemyObject,spawnPoint, Quaternion.identity);

    int currentWeight = Random.Range(0, totalWeights);
    int currentListPosition = 0;

    while (currentWeight < totalWeights)
    {
        currentWeight += enemyDatas[currentListPosition].SpawnerWeight;
        currentListPosition++;
    }
    Debug.Log(currentListPosition.ToString() + "position");
    currentEnemy.GetComponent<EnemyController>().EnemyData = enemyDatas[currentListPosition];
}

//public Vector3 RandomNavmeshLocation(float radius)
//{
//    Vector3 randomDirection = Random.insideUnitSphere * radius;
//    randomDirection += transform.position;
//    NavMeshHit hit;
//    Vector3 finalPosition = Vector3.zero;
//    int count = 0;
//    do
//    {
//        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
//            finalPosition = hit.position;

//        Debug.Log(count.ToString());
//        count++;
//        if (count >= 10)
//        {
//            break;
//        }

//    } while (finalPosition != null);

//    return finalPosition;
//}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 4, time = 1.5f;

    public GameObject[] enemies;

    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
        StartCoroutine(DifficultyIncrease());
    }
    
    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(time) ;
        StartCoroutine(SpawnAnEnemy());
    }

    IEnumerator DifficultyIncrease()
    {
        yield return new WaitForSeconds(1);
        time = time - (time * 0.01f);
        StartCoroutine(DifficultyIncrease());
    }
}

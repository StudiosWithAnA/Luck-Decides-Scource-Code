using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Enemy> enemies;

    private GameObject[] spawnPoints;

    public GameObject[] enemyObj;
    public GameObject[] item;

    public int maxEnemies;

    public float startTimeBtwSpawnEnemies;
    private float timeBtwSpawinEnemies;
    public float minTimeBtwSpawn;

    public float startTimeBtwItemSpawn;
    private float TimeBtwItemSpawn;

    public bool spawning = true;

    Manager manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        timeBtwSpawinEnemies = startTimeBtwSpawnEnemies;
        TimeBtwItemSpawn = startTimeBtwItemSpawn;
        enemies = new List<Enemy>();
    }

    
    void Update()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

        if (manager.isOver == true)
            return;

        if (timeBtwSpawinEnemies > 0)
            timeBtwSpawinEnemies -= Time.deltaTime;

        if(timeBtwSpawinEnemies <= 0)
        {
            Spawn();
        }

        if (!spawning)
            return;

        if (TimeBtwItemSpawn > 0)
            TimeBtwItemSpawn -= Time.deltaTime;

        if(TimeBtwItemSpawn <= 0)
        {
            SpawnItem();
        }

    }

    void Spawn()
    {
        if (enemies.Count > maxEnemies)
            return;

        int rando = Random.Range(0, spawnPoints.Length);
        int randoEnemy = Random.Range(0, enemyObj.Length);
        GameObject obj = Instantiate(enemyObj[randoEnemy], spawnPoints[rando].transform.position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemies.Add(enemy);
        enemy.spawner = this;
        timeBtwSpawinEnemies = startTimeBtwSpawnEnemies;

        if(startTimeBtwSpawnEnemies > minTimeBtwSpawn)
        startTimeBtwSpawnEnemies -= .1f;
    }

    void SpawnItem()
    {
        int rando = Random.Range(0, spawnPoints.Length);
        int randoItem = Random.Range(0, item.Length);
        Instantiate(item[randoItem], spawnPoints[rando].transform.position, Quaternion.identity);
        TimeBtwItemSpawn = startTimeBtwItemSpawn;
    }
}

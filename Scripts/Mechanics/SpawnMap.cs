using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private GameObject[] map;
    [SerializeField] private Transform spawnPos;
    private void Awake()
    {
        Spawn();
    }

    void Spawn()
    {
        int Rando = Random.Range(0, map.Length); ;
        Instantiate(map[Rando], spawnPos.position, Quaternion.identity);
    }
}

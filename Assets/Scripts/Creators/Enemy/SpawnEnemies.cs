using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] GameObject prefab;
    [SerializeField] float delayMin;
    [SerializeField] float delayMax;
    [SerializeField] float spawnDistance;
    float timer;

    // Update is called once per frame
    void Update ()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            GameObject enemy = Instantiate(prefab, position * spawnDistance, Quaternion.identity, transform);
            enemy.name = prefab.name;

            timer = Random.Range(delayMin, delayMax);
        }
    }
}

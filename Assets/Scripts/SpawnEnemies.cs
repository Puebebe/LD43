using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
	[SerializeField] GameObject enemy;
	[SerializeField] float delayMax;
	[SerializeField] float delayMin;
	float timer;

	// Update is called once per frame
	void Update () {
		if (timer > 0)
			timer -= Time.deltaTime;
		else
		{
			Vector3 position = new Vector3 (Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
			Instantiate(enemy, position * 8, Quaternion.identity, transform);

			timer = Random.Range(delayMax,delayMin);
		}
	}
}

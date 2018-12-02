using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	[SerializeField] GameObject target;
	[SerializeField] float damage;
	[SerializeField] float step;

	// Use this for initialization
	void Start () {

		target = GameObject.Find("Base");

		transform.rotation = Quaternion.identity;
		transform.LookAt(target.transform);
		transform.Rotate(0,0,90);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

		if(Vector3.Distance(target.transform.position, transform.position) < 1)
		{
			EnergyBarBehavior.Energy -= damage;
			Destroy(gameObject);
		}
	}
}

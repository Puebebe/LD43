using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehavior : MonoBehaviour
{
	[SerializeField] GameObject target;
	[SerializeField] float damage;
	[SerializeField] float speedMax;
    [SerializeField] float speedMin;
    float speed;

    // Use this for initialization
    void Start ()
    {
        target = transform.parent.GetComponent<SpawnEnemies>().target;
        speed = Random.Range(speedMin, speedMax);

		transform.rotation = Quaternion.identity;
		transform.LookAt(target.transform);
		transform.Rotate(0, 0, 90);
	}

	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            EnergyBarBehavior.Energy -= damage;
            Destroy(gameObject);
            Handheld.Vibrate();
        }
    }
}

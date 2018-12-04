using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBehavior : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float destroyDistance;
    [SerializeField] GameObject prefab;

	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.parent.position, transform.position) > destroyDistance)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == enemy.name)
        {
            GameObject explosion = Instantiate(prefab, transform.position, Quaternion.identity);
            explosion.transform.Rotate(Vector3.forward, Random.Range(-180,180));
            Destroy(explosion, 3);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

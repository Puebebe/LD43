using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBehavior : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float destroyDistance;
	
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

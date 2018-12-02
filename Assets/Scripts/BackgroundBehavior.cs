using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour {

	[SerializeField] float speed;

	void Update () {
  	gameObject.transform.Rotate(0,0, speed);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationRotator : MonoBehaviour
{
    [SerializeField] GameObject station;
    [SerializeField] Joystick joystick;

	// Update is called once per frame
	void Update ()
    {
        Vector3 rotation = new Vector3(joystick.Direction.x, joystick.Direction.y);
        Vector3 currentRotation = station.transform.up;
        float angle = Vector3.SignedAngle(currentRotation, rotation, Vector3.back);
        station.transform.Rotate(Vector3.back, angle);
    }
}

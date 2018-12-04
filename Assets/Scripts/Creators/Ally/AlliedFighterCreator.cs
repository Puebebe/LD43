using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedFighterCreator : MonoBehaviour
{
    [SerializeField] GameObject station;
    [SerializeField] GameObject hangarManager;
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject prefab;
	[SerializeField] float speed;
    [SerializeField] float cost;
    [SerializeField] float delay;
    float timer;
	
	// Update is called once per frame
	void Update ()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Debug.Log(touch.phase);

            if (touch.phase == TouchPhase.Stationary && joystick.Direction != Vector2.zero)
            {
                var parent = hangarManager.transform;
                GameObject fighter = Instantiate(prefab, parent.position, station.transform.rotation, parent);
                fighter.transform.Rotate(Vector3.right, 90);

                var rb = fighter.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.AddForce(new Vector3(joystick.Direction.x, joystick.Direction.y).normalized * speed);

                EnergyBarBehavior.Energy -= cost;
                timer = delay;
            }
        }
	}
}

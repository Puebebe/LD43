using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedFighterCreator : MonoBehaviour
{
    //[SerializeField] GameObject hangar;
    [SerializeField] Joystick joystick;
    float timer;
    float delay;

    // Use this for initialization
    void Start () {
        delay = 0.5f;
	}
	
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
                GameObject fighter = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //fighter.transform.SetParent(hangar.transform);
                
                fighter.AddComponent<Rigidbody>();
                var rb = fighter.GetComponent<Rigidbody>();
                rb.useGravity = false;
                fighter.GetComponent<Collider>().isTrigger = true;
                rb.AddForce(new Vector3(joystick.Direction.x, joystick.Direction.y).normalized * 100);
                
                timer = delay;
            }
        }
	}

    
}

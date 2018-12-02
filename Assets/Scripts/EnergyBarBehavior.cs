using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarBehavior : MonoBehaviour
{
	[SerializeField] float regeneration;
	public SimpleHealthBar energyBar;
 	private static float energy = 100;
	public static float Energy
	{
		get{ return energy; }
		set{ energy = Mathf.Clamp(value, -10f ,100f);	}
	}

	// Update is called once per frame
	void Update () {
		Energy += regeneration;
		energyBar.UpdateBar(energy,100);
	}
}

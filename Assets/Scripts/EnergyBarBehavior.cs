using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarBehavior : MonoBehaviour
{
    [SerializeField] private float regeneration;
    private static bool isRegenerationOn = true;
    public SimpleHealthBar energyBar;
    private static float energy = 100;
    public static float Energy
    {
        get { return energy; }
        set
        {
            if (value <= 0)
            {
                if (!Game.IsOn)
                    return;
                
                isRegenerationOn = false;
                Game.EndGame();
            }
            energy = Mathf.Clamp(value, 0f, 100f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (isRegenerationOn)
            Energy += regeneration;
        energyBar.UpdateBar(energy, 100);
    }
}

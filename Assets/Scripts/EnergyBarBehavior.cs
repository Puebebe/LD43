using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarBehavior : MonoBehaviour
{
    [SerializeField] private float regeneration;
    public static bool isRegenerationOn = true;
    [SerializeField] private SimpleHealthBar energyBar;
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

    public static IEnumerator EnergyLoading()
    {
        while (energy < 100)
        {
            Energy++;
            yield return null;
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

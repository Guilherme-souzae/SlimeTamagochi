using System.Collections;
using UnityEngine;

public class SlimeLogic : MonoBehaviour
{
    [Header("Valores dos atributos")]
    public int ph;
    public int humidity;
    public int hunger;
    public int energy;
    public int secondsToDie;

    [Header("Danger Values:")]
    [Header("PH")]
    [Range(0, 100)] public int PH_DANGER_LOW;
    [Range(0, 100)] public int PH_DANGER_HIGH;
    [Header("Humidity")]
    [Range(0, 100)] public int HUMIDITY_DANGER_LOW;
    [Range(0, 100)] public int HUMIDITY_DANGER_HIGH;
    [Header("Hunger")]
    [Range(0, 100)] public int HUNGER_DANGER_LOW;
    [Range(0, 100)] public int HUNGER_DANGER_HIGH;
    [Header("Energy")]
    [Range(0, 100)] public int ENERGY_DANGER_LOW;
    [Range(0, 100)] public int ENERGY_DANGER_HIGH;

    [Header("Flags de omeostase")]
    public bool generalOmeostasis;
    public bool isInCystForm;
    public bool phOmeostasis;
    public bool humidityOmeostasis;
    public bool hungerOmeostasis;
    public bool energyOmeostasis;

    public static SlimeLogic Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Increase functions
    public void IncreasePh(int x)
    {
        if (ph + x >=0 && ph + x <= 100)
        {
            ph += x;
        }
    }

    public void IncreaseHumidity(int x)
    {
        if (humidity + x >= 0 && humidity + x <= 100)
        {
            humidity += x;
        }
    }

    public void IncreaseHunger(int x)
    {
        if (hunger + x >= 0 && hunger + x <= 100)
        {
            hunger += x;
        }
    }

    public void IncreaseEnergy(int x)
    {
        if (energy + x >= 0 &&  energy + x <= 100)
        {
            energy += x;
        }
    }

    private void checkOmeostasis()
    {
        phOmeostasis = (ph <= PH_DANGER_LOW || ph >= PH_DANGER_HIGH) ? false : true;
        humidityOmeostasis = (humidity <= HUMIDITY_DANGER_LOW || humidity >= HUMIDITY_DANGER_HIGH) ? false : true;
        hungerOmeostasis = (hunger <= HUNGER_DANGER_LOW || hunger >= HUNGER_DANGER_HIGH) ? false : true;
        energyOmeostasis = (energy <= ENERGY_DANGER_LOW || energy >= ENERGY_DANGER_HIGH) ? false : true;

        generalOmeostasis = phOmeostasis && humidityOmeostasis && hungerOmeostasis && energyOmeostasis;
    }
}

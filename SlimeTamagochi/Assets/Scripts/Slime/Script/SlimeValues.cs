using System.Collections;
using UnityEngine;

public class SlimeValues : MonoBehaviour
{
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

    public static SlimeValues Instance;

    public SlimeStats stats;

    private void Awake()
    {
        Instance = this;
    }

    // Increase functions
    public void IncreasePh(int x)
    {
        if (stats.ph + x >= 0 && stats.ph + x <= 100)
        {
            stats.ph += x;
        }
        else
        {
            stats.ph = (stats.ph + x < 0) ? 0 : 100;
        }
        checkOmeostasis();
    }

    public void IncreaseHumidity(int x)
    {
        if (stats.humidity + x >= 0 && stats.humidity + x <= 100)
        {
            stats.humidity += x;
        }
        else
        {
            stats.humidity = (stats.humidity + x < 0) ? 0 : 100;
        }
        checkOmeostasis();
    }

    public void IncreaseHunger(int x)
    {
        if (stats.hunger + x >= 0 && stats.hunger + x <= 100)
        {
            stats.hunger += x;
        }
        else
        {
            stats.hunger = (stats.hunger + x < 0) ? 0 : 100;
        }
        checkOmeostasis();
    }

    public void IncreaseEnergy(int x)
    {
        if (stats.energy + x >= 0 && stats.energy + x <= 100)
        {
            stats.energy += x;
        }
        else
        {
            stats.energy = (stats.energy + x < 0) ? 0 : 100;
        }
        checkOmeostasis();
    }

    public void Consume(ItemData meal)
    {
        if (meal == null) return;

        IncreasePh(meal.ph);
        IncreaseHumidity(meal.humidity);
        IncreaseHunger(meal.hunger);
        IncreaseEnergy(meal.energy);
    }

    // Check state function
    private void checkOmeostasis()
    {
        phOmeostasis = (stats.ph <= PH_DANGER_LOW || stats.ph >= PH_DANGER_HIGH) ? false : true;
        humidityOmeostasis = (stats.humidity <= HUMIDITY_DANGER_LOW || stats.humidity >= HUMIDITY_DANGER_HIGH) ? false : true;
        hungerOmeostasis = (stats.hunger <= HUNGER_DANGER_LOW || stats.hunger >= HUNGER_DANGER_HIGH) ? false : true;
        energyOmeostasis = (stats.energy <= ENERGY_DANGER_LOW || stats.energy >= ENERGY_DANGER_HIGH) ? false : true;

        generalOmeostasis = phOmeostasis && humidityOmeostasis && hungerOmeostasis && energyOmeostasis;

        ShowDebug(); // A função é chamada sempre que o estado do slime é atualizado
    }

    // Get and Set stats functions
    public SlimeStats GetStats()
    {
        return stats;
    }

    public void SetStats(SlimeStats newStats)
    {
        stats = newStats;
    }

    // Debug Function
    private void ShowDebug()
    {
        Debug.Log("ESTADO DO SLIME ATUALIZADO");
        Debug.Log("PH: " + stats.ph);
        Debug.Log("Humidity: " + stats.humidity);
        Debug.Log("Hunger: " + stats.hunger);
        Debug.Log("Energy: " + stats.energy);
    }
}

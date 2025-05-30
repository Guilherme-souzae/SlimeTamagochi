using UnityEngine;
using TMPro;

public enum ValueState
{
    IDLE,
    SLEEPING
}

public class SlimeValues : MonoBehaviour
{
    public static SlimeValues Instance;
    [Header("Dependencias UI")]
    public TextMeshProUGUI UIph;
    public TextMeshProUGUI UIhumidity;
    public TextMeshProUGUI UIhunger;
    public TextMeshProUGUI UIenergy;
    
    [Header("Limites perigosos")]
    [Range(0, 100)] public int PH_DANGER_LOW, PH_DANGER_HIGH;
    [Range(0, 100)] public int HUMIDITY_DANGER_LOW, HUMIDITY_DANGER_HIGH;
    [Range(0, 100)] public int HUNGER_DANGER_LOW, HUNGER_DANGER_HIGH;
    [Range(0, 100)] public int ENERGY_DANGER_LOW, ENERGY_DANGER_HIGH;

    [Header("Estado de homeostase")]
    public bool generalOmeostasis;
    public bool isInCystForm;
    public bool phOmeostasis;
    public bool humidityOmeostasis;
    public bool hungerOmeostasis;
    public bool energyOmeostasis;

    public SlimeStats stats;
    private ValueState state;

    private void Awake() => Instance = this;

    public void SetState(ValueState newState)
    {
        if (newState != state)
        {
            state = newState;
        }
    }

    private int ClampStat(int value) => Mathf.Clamp(value, 0, 100);

    public void IncreasePh(int x)
    {
        stats.ph = ClampStat(stats.ph + x);
        checkOmeostasis();
    }

    public void IncreaseHumidity(int x)
    {
        stats.humidity = ClampStat(stats.humidity + x);
        checkOmeostasis();
    }

    public void IncreaseHunger(int x)
    {
        stats.hunger = ClampStat(stats.hunger + x);
        checkOmeostasis();
    }

    public void IncreaseEnergy(int x)
    {
        int delta = (state == ValueState.SLEEPING) ? -x : x;
        stats.energy = ClampStat(stats.energy + delta);
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

    private void checkOmeostasis()
    {
        phOmeostasis = !(stats.ph <= PH_DANGER_LOW || stats.ph >= PH_DANGER_HIGH);
        humidityOmeostasis = !(stats.humidity <= HUMIDITY_DANGER_LOW || stats.humidity >= HUMIDITY_DANGER_HIGH);
        hungerOmeostasis = !(stats.hunger <= HUNGER_DANGER_LOW || stats.hunger >= HUNGER_DANGER_HIGH);
        energyOmeostasis = !(stats.energy <= ENERGY_DANGER_LOW || stats.energy >= ENERGY_DANGER_HIGH);

        generalOmeostasis = phOmeostasis && humidityOmeostasis && hungerOmeostasis && energyOmeostasis;

        ShowDebug();
    }

    public SlimeStats GetStats() => stats;
    public void SetStats(SlimeStats newStats) => stats = newStats;

    private void ShowDebug()
    {
        UIph.text = "Ph: " + stats.ph;
        UIhumidity.text = "Hum: " + stats.humidity;
        UIhunger.text = "Hun: " + stats.hunger;
        UIenergy.text = "Energy: " + stats.energy;
    }
}

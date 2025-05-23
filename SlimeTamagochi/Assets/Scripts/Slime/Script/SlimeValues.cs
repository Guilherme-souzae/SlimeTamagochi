using UnityEngine;

public enum ValueState
{
    IDLE,
    SLEEPING,
    CYST
}

public class SlimeValues : MonoBehaviour
{
    public static SlimeValues Instance;

    [Header("Limites perigosos")]
    [Range(0, 100)] public int PH_DANGER_LOW, PH_DANGER_HIGH;
    [Range(0, 100)] public int HUMIDITY_DANGER_LOW, HUMIDITY_DANGER_HIGH;
    [Range(0, 100)] public int HUNGER_DANGER_LOW, HUNGER_DANGER_HIGH;
    [Range(0, 100)] public int ENERGY_DANGER_LOW, ENERGY_DANGER_HIGH;

    [Header("Estado de homeostase")]
    public bool phOmeostasis;
    public bool humidityOmeostasis;
    public bool hungerOmeostasis;
    public bool energyOmeostasis;

    public SlimeStats stats;
    private ValueState state;
    private bool isDying;
    
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
        if (state == ValueState.CYST) return;
        stats.ph = ClampStat(stats.ph + x);
        checkOmeostasis();
    }

    public void IncreaseHumidity(int x)
    {
        if (state == ValueState.CYST) return;
        stats.humidity = ClampStat(stats.humidity + x);
        checkOmeostasis();
    }

    public void IncreaseHunger(int x)
    {
        if (state == ValueState.CYST) return;
        stats.hunger = ClampStat(stats.hunger + x);
        checkOmeostasis();
    }

    public void IncreaseEnergy(int x)
    {
        if (state == ValueState.CYST) return;

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

        bool tempIsDying = !(phOmeostasis && humidityOmeostasis && hungerOmeostasis && energyOmeostasis);

        if (tempIsDying && !isDying)
        {
            isDying = true;
            Debug.Log("⚠️ Slime em risco! Iniciando contagem para morte.");
            SlimeTimers.Instance?.StartDyingCountdown();
        }
        else if (!tempIsDying && isDying)
        {
            isDying = false;
            Debug.Log("✅ Slime está seguro. Cancelando contagem de morte.");
            SlimeTimers.Instance?.CeaseDyingCountdown();
        }

        ShowDebug();
    }

    public SlimeStats GetStats() => stats;
    public void SetStats(SlimeStats newStats) => stats = newStats;

    private void ShowDebug()
    {
        Debug.Log($"ESTADO DO SLIME ATUALIZADO\nPH: {stats.ph}, Humidity: {stats.humidity}, Hunger: {stats.hunger}, Energy: {stats.energy}");
    }
}

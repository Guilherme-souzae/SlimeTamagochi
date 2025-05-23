using System;
using System.Collections;
using UnityEngine;

public class SlimeTimers : MonoBehaviour
{
    public static SlimeTimers Instance;

    [Header("Contagem de morte em minutos")]
    public int DEATH_COUNTDOWN;
    
    [Header("Taxas de variação")]
    [Range(-100, 100)] public int PH_CHANGE_RATE;
    [Range(-100, 100)] public int HUMIDITY_CHANGE_RATE;
    [Range(-100, -1)] public int HUNGER_CHANGE_RATE;
    [Range(-100, -1)] public int ENERGY_CHANGE_RATE;

    [Header("Tempos de variação em minutos")]
    [Range(1, 60)] public int PH_CHANGE_TIME;
    [Range(1, 60)] public int HUMIDITY_CHANGE_TIME;
    [Range(1, 60)] public int HUNGER_CHANGE_TIME;
    [Range(1, 60)] public int ENERGY_CHANGE_TIME;

    [Header("Divisor de tempo")]
    [Range(1, 60)] public int TIME_DIVISOR = 60;
    
    private Coroutine dyingCoroutine;
    
    private void Awake() => Instance = this;

    private void Start()
    {
        var buffer = SaveSystem.LoadSlime();
        if (buffer != null) PassiveUpdate(buffer);

        StartCoroutine(ActivePhUpdate());
        StartCoroutine(ActiveHumidityUpdate());
        StartCoroutine(ActiveHungerUpdate());
        StartCoroutine(ActiveEnergyUpdate());
    }

    private void OnApplicationQuit() => SaveSystem.SaveSlime();

    public void StartDyingCountdown()
    {
        if (dyingCoroutine == null)
        {
            dyingCoroutine = StartCoroutine(DyingCountdown());
        }
    }

    public void CeaseDyingCountdown()
    {
        if (dyingCoroutine != null)
        {
            StopCoroutine(dyingCoroutine);
            dyingCoroutine = null;
        }
    }

    private IEnumerator DyingCountdown()
    {
        yield return new WaitForSeconds(DEATH_COUNTDOWN * TIME_DIVISOR);
        Die();
    }

    private void Die()
    {
        Debug.Log("Slime morreu");
        SlimeValues.Instance.SetState(ValueState.CYST);
        SlimeBehavior.Instance.SetState(BehaviorState.CYST);
        SaveSystem.SaveSlime();
    }

    
    private IEnumerator ActivePhUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(PH_CHANGE_TIME * TIME_DIVISOR);
            SlimeValues.Instance?.IncreasePh(PH_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHumidityUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUMIDITY_CHANGE_TIME * TIME_DIVISOR);
            SlimeValues.Instance?.IncreaseHumidity(HUMIDITY_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHungerUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUNGER_CHANGE_TIME * TIME_DIVISOR);
            SlimeValues.Instance?.IncreaseHunger(HUNGER_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveEnergyUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(ENERGY_CHANGE_TIME * TIME_DIVISOR);
            SlimeValues.Instance?.IncreaseEnergy(ENERGY_CHANGE_RATE);
        }
    }

    private void PassiveUpdate(DataHolder buffer)
    {
        int lastTime = buffer.lastTime;
        int currTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        int deltaTimeMinutes = (currTime - lastTime) / TIME_DIVISOR;

        var stats = new SlimeStats(buffer.stats[0], buffer.stats[1], buffer.stats[2], buffer.stats[3]);
        SlimeValues.Instance?.SetStats(stats);

        if (buffer.isSleeping)
        {
            SlimeValues.Instance?.SetState(ValueState.SLEEPING);
        }

        SlimeValues.Instance?.IncreasePh(deltaTimeMinutes * PH_CHANGE_RATE / PH_CHANGE_TIME);
        SlimeValues.Instance?.IncreaseHumidity(deltaTimeMinutes * HUMIDITY_CHANGE_RATE / HUMIDITY_CHANGE_TIME);
        SlimeValues.Instance?.IncreaseHunger(deltaTimeMinutes * HUNGER_CHANGE_RATE / HUNGER_CHANGE_TIME);
        SlimeValues.Instance?.IncreaseEnergy(deltaTimeMinutes * ENERGY_CHANGE_RATE / ENERGY_CHANGE_TIME);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeTimers : MonoBehaviour
{
    public static SlimeTimers Instance;

    [Header("Taxas de variação:")]
    [Range(-100, 100)] public int PH_CHANGE_RATE;
    [Range(-100, 100)] public int HUMIDITY_CHANGE_RATE;
    [Range(-100, 100)] public int HUNGER_CHANGE_RATE;
    [Range(-100, 100)] public int ENERGY_CHANGE_RATE;

    [Header("Tempos de variação em minutos:")]
    [Range(1, 60)] public int PH_CHANGE_TIME;
    [Range(1, 60)] public int HUMIDITY_CHANGE_TIME;
    [Range(1, 60)] public int HUNGER_CHANGE_TIME;
    [Range(1, 60)] public int ENERGY_CHANGE_TIME;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(ActivePhUpdate());
    }

    private IEnumerator ActivePhUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(PH_CHANGE_TIME);
            Debug.Log("Atualizando PH");
            SlimeLogic.Instance.IncreasePh(PH_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHumidityUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUMIDITY_CHANGE_TIME);
            Debug.Log("Atualizando umidade");
            SlimeLogic.Instance.IncreaseHumidity(HUMIDITY_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHungerUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUNGER_CHANGE_TIME);
            Debug.Log("Atualizando fome");
            SlimeLogic.Instance.IncreaseHunger(HUNGER_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveEnergyUpdate()
    {
        yield return new WaitForSeconds(ENERGY_CHANGE_TIME);
        Debug.Log("Atualizando energia");
        SlimeLogic.Instance.IncreaseEnergy(ENERGY_CHANGE_RATE);
    }

    public void PassiveStatsUpdate(DateTime lastTime)
    {
        DateTime currTime = DateTime.Now;
        TimeSpan deltaTime = currTime - lastTime;
        int totalMins = (int)deltaTime.TotalMinutes;

        int phChanges = totalMins / PH_CHANGE_TIME;
        int humidityChanges = totalMins / HUMIDITY_CHANGE_TIME;
        int hungerChanges = totalMins / HUNGER_CHANGE_TIME;
        int energyChanges = totalMins / ENERGY_CHANGE_TIME;

        SlimeLogic.Instance.IncreasePh(phChanges * PH_CHANGE_RATE);
        SlimeLogic.Instance.IncreaseHumidity(humidityChanges * HUMIDITY_CHANGE_RATE);
        SlimeLogic.Instance.IncreaseHunger(hungerChanges * HUNGER_CHANGE_RATE);
        SlimeLogic.Instance.IncreasePh(energyChanges * ENERGY_CHANGE_RATE);
    }
}

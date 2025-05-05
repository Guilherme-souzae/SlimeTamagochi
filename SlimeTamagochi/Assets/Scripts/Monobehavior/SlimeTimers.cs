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

    [Header("Divisor de tempo")]
    [Header("Fora do modo de testes, deixar o divisor sempre em 60")]
    [Range(1, 60)] public int TIME_DIVISOR = 60;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataHolder buffer = SaveSystem.LoadSlime();
        if (buffer != null )
        {
            PassiveUpdate(buffer);
        }

        StartCoroutine(ActivePhUpdate());
        StartCoroutine(ActiveHumidityUpdate());
        StartCoroutine(ActiveHungerUpdate());
        StartCoroutine(ActiveEnergyUpdate());
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveSlime();
    }

    private IEnumerator ActivePhUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(PH_CHANGE_TIME * TIME_DIVISOR);
            Debug.Log("Atualizando PH");
            SlimeLogic.Instance.IncreasePh(PH_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHumidityUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUMIDITY_CHANGE_TIME * TIME_DIVISOR);
            Debug.Log("Atualizando umidade");
            SlimeLogic.Instance.IncreaseHumidity(HUMIDITY_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveHungerUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(HUNGER_CHANGE_TIME * TIME_DIVISOR);
            Debug.Log("Atualizando fome");
            SlimeLogic.Instance.IncreaseHunger(HUNGER_CHANGE_RATE);
        }
    }

    private IEnumerator ActiveEnergyUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(ENERGY_CHANGE_TIME * TIME_DIVISOR);
            Debug.Log("Atualizando energia");
            SlimeLogic.Instance.IncreaseEnergy(ENERGY_CHANGE_RATE);
        }
    }

    private void PassiveUpdate(DataHolder buffer)
    {
        int lastTime = buffer.lastTime;
        int currTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        int deltaTimeMinutes = (int)((currTime - lastTime) / TIME_DIVISOR);

        int phChange = deltaTimeMinutes * PH_CHANGE_RATE / PH_CHANGE_TIME;
        int humidityChange = deltaTimeMinutes * HUMIDITY_CHANGE_RATE / HUMIDITY_CHANGE_TIME;
        int hungerChange = deltaTimeMinutes * HUNGER_CHANGE_RATE / HUNGER_CHANGE_TIME;
        int energyChange = deltaTimeMinutes * ENERGY_CHANGE_RATE / ENERGY_CHANGE_TIME;

        SlimeStats stats = new SlimeStats(buffer.stats[0], buffer.stats[1], buffer.stats[2], buffer.stats[3]);
        SlimeLogic.Instance.SetStats(stats);

        SlimeLogic.Instance.IncreasePh(phChange);
        SlimeLogic.Instance.IncreaseHumidity(humidityChange);
        SlimeLogic.Instance.IncreaseHunger(hungerChange);
        SlimeLogic.Instance.IncreaseEnergy(energyChange);
    }
}

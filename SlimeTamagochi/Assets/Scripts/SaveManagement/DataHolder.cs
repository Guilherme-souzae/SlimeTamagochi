
using System;
using UnityEngine;

[System.Serializable]
public class DataHolder
{
    public int lastTime;
    public bool isSleeping;
    public bool isCyst;
    public int[] stats;

    public DataHolder()
    {
        lastTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        isSleeping = (SlimeBehavior.Instance.GetSleeping()) ? true : false;
        isCyst = (SlimeBehavior.Instance.GetState() == BehaviorState.CYST) ? true : false;
        stats = new int[4];
        stats[0] = SlimeValues.Instance.stats.ph;
        stats[1] = SlimeValues.Instance.stats.humidity;
        stats[2] = SlimeValues.Instance.stats.hunger;
        stats[3] = SlimeValues.Instance.stats.energy;
    }
}

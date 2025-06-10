
using System;
using UnityEngine;

[System.Serializable]
public class DataHolder
{
    public int lastTime;
    public bool isSleeping;
    public int[] stats;

    public DataHolder()
    {
        lastTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        isSleeping = (SlimeBehavior.Instance.GetState() == BehaviorState.SLEEPING || SlimeBehavior.Instance.GetState() == BehaviorState.GOING_TO_SLEEP);
        stats = new int[4];
        stats[0] = SlimeValues.Instance.stats.ph;
        stats[1] = SlimeValues.Instance.stats.humidity;
        stats[2] = SlimeValues.Instance.stats.hunger;
        stats[3] = SlimeValues.Instance.stats.energy;
    }
    
    public DataHolder(int ph, int hum, int hung, int energy)
    {
        lastTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        isSleeping = false;
        stats = new int[4];
        stats[0] = ph;
        stats[1] = hum;
        stats[2] = hung;
        stats[3] = energy;
    }
}

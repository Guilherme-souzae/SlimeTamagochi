
using System;

[System.Serializable]
public class DataHolder
{
    public int lastTime;
    public int[] stats;

    public DataHolder()
    {
        lastTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        stats = new int[4];
        stats[0] = SlimeLogic.Instance.stats.ph;
        stats[1] = SlimeLogic.Instance.stats.humidity;
        stats[2] = SlimeLogic.Instance.stats.hunger;
        stats[3] = SlimeLogic.Instance.stats.energy;
    }
}

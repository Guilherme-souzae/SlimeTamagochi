using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLogic : MonoBehaviour
{
    public static SlimeLogic Instance;

    private float ph = 7f;
    private float humidity = 500;

    private void Awake()
    {
        Instance = this;
    }

    public void increasePh(float xPh)
    {
        ph += xPh;
    }

    public void increaseHumidity(float xHumidity)
    {
        humidity += xHumidity;
    }
}

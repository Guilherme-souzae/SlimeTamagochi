using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhSliderBehavior : MonoBehaviour
{
    [Header("Configurações do minigame")]
    [Range(-100, -1)] public int minValue = -50;
    [Range(1, 100)] public int maxValue = 50;
    public float speed = 0.5f;

    public Slider phSlider;
    private bool running = false;
    private bool increase = true;

    private void Start()
    {
        phSlider.interactable = false;
    }

    private void OnEnable()
    {
        increase = true;
        running = true;
        phSlider.enabled = true;
        phSlider.value = phSlider.maxValue / 2f;
    }

    private void OnDisable()
    {
        running = false;
    }

    private void Update()
    {
        if (running)
        {
            if (increase)
            {
                phSlider.value += speed * Time.deltaTime;
                if (phSlider.value >= phSlider.maxValue)
                {
                    phSlider.value = phSlider.maxValue;
                    increase = false;
                }
            }
            else
            {
                phSlider.value -= speed * Time.deltaTime;
                if (phSlider.value <= phSlider.minValue)
                {
                    phSlider.value = phSlider.minValue;
                    increase = true;
                }
            }
        }
    }

    public void updateSlimeTroughtMinigame()
    {
        float buffer = phSlider.value;
        buffer = minValue + (buffer * (maxValue - minValue));
        int final = Mathf.RoundToInt(buffer);

        SlimeValues.Instance.IncreasePh(final);
    }
}

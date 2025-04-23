using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhSliderBehavior : MonoBehaviour
{
    public Slider phSlider;
    public float speed = 0.5f;
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
}

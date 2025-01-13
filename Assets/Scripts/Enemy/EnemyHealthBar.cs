﻿using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyHealthBar : MonoBehaviour, IHealthBarView
{
    private const float MaxSliderValue = 1.0f;
    private const float MinSliderValue = 0.0f;

    private Slider _slider;
    private int _maxHealth;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = MaxSliderValue;
        _slider.minValue = MinSliderValue;
    }

    public void SetMaxHealth(int health)
    {
        if (health <= 0)
            throw new ArgumentOutOfRangeException(nameof(health)); 

        _maxHealth = health;
        _slider.value = MaxSliderValue;
    }

    public void UpdateDataHealth(int currentHealth)
    {
        if (currentHealth > _maxHealth || currentHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(currentHealth));

        _slider.value = (float)currentHealth / _maxHealth;
    }
}
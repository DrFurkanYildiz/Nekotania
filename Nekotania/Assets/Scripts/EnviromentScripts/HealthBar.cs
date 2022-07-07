using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    float maxHealth = 100f;
    float lerpSpeed;
    public Gradient gradient;
    void Start()
    {
        //health = maxHealth;
    }

    void Update()
    {
        HealthBarFiller();
        ColorChanger();
        if (health > maxHealth) health = maxHealth;
        lerpSpeed = 3f * Time.deltaTime;
    }

    private void ColorChanger()
    {
        healthBar.color = gradient.Evaluate(health / maxHealth);
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }
    public void Damage(float damagePoint)
    {
        if (health > 0)
            health -= damagePoint;
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}

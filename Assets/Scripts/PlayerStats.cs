using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public StatusBar HealthBar;
    public StatusBar StaminaBar;
    public StatusBar ExperienceBar;

    public int maxHealth = 100;
    public int health;

    public int maxStamina = 50;
    public int stamina;

    public int maxExperience = 1000;
    public int experience = 0;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        HealthBar.setMaxValue(maxHealth);
        HealthBar.setValue(health);

        stamina = maxStamina;
        StaminaBar.setMaxValue(maxStamina);
        StaminaBar.setValue(stamina);

        ExperienceBar.setMaxValue(maxExperience);
        ExperienceBar.setValue(experience);

        InvokeRepeating("RegenerateHealth", 0.0f, 0.3f);
        InvokeRepeating("RegenerateStamina", 0.0f, 0.08f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.X))
            ReduceStamina(10);
        if (Input.GetKeyDown(KeyCode.C))
            GainExperience(100);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        HealthBar.setValue(health);
    }

    void Heal(int amount)
    {
        if (health == maxHealth)
            return;
        else if (health + amount > maxHealth)
            health = maxHealth;
        else
            health += amount;
        HealthBar.setValue(health);
    }

    public void ReduceStamina(int amount)
    {
        stamina -= amount;
        StaminaBar.setValue(stamina);
    }

    void RecoverStamina(int amount)
    {
        if (stamina == maxStamina)
            return;
        else if (stamina + amount > maxStamina)
            stamina = maxStamina;
        else
            stamina += amount;
        StaminaBar.setValue(stamina);
    }

    void RegenerateHealth()
    {
        if (health == maxHealth)
            return;
        else
            health += 1;
        HealthBar.setValue(health);

    }
    void RegenerateStamina()
    {
        if (stamina == maxStamina)
            return;
        else
            stamina += 1;
        StaminaBar.setValue(stamina);
    }

    public void GainExperience(int amount)
    {
        if (experience + amount >= maxExperience)
        {
            experience = experience + amount - maxExperience;
            LevelUp();
        }
        else
            experience += amount;
        ExperienceBar.setValue(experience);
    }

    void LevelUp()
    {
        level++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public GameObject HealthBar;

    public int maxHealth;
    public int health;

    public int level;
    public int experienceGain;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        print(health);
        Transform fillBar = HealthBar.transform.GetChild(0).GetChild(0);
        fillBar.localScale = new Vector3((health * fillBar.localScale.x)/maxHealth,
            fillBar.localScale.y, fillBar.localScale.z);
    }
}

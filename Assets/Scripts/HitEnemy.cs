using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" && Player.GetComponent<PlayerMovement>().button_fightPressed)
        {
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(40);
            if(other.gameObject.GetComponent<EnemyStats>().health <= 0)
            {
                Player.GetComponent<PlayerStats>().GainExperience(other.gameObject.GetComponent<EnemyStats>().experienceGain);
                Destroy(other.gameObject);
            }
        }
    }
}

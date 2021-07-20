using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public Animator animator;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isFighting", true);
            player.GetComponent<PlayerStats>().TakeDamage(10);
            if (player.GetComponent<PlayerStats>().health <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

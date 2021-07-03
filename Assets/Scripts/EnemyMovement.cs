using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    public float waitTime = 2f;
    public float directionChangeTime = 1f;
    public float moveSpeed = 2f;

    private float latestDirectionChangeTime;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private bool isWaiting;

    public Vector3 bottomLeftLimit;
    public Vector3 topRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("Enemy_1").GetComponent<Rigidbody2D>();
        isWaiting = false;
        latestDirectionChangeTime = 0f;
        CalcuateNewMovementVector();
    }

    // Update is called once per frame
    void Update()
    {
        // If the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime && !isWaiting)
        {
            isWaiting = true;
            Invoke("InvokeIsWaiting", waitTime);
            movementPerSecond = Vector3.zero;
        }
        if (isWaiting == true)
        {
            animator.SetBool("isWalking", false);
        }
        else if (isWaiting == false)
        {
            animator.SetBool("isWalking", true);
        }

        // $$anonymous$$ove enemy 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y + (movementPerSecond.y * Time.deltaTime));

        // $$anonymous$$eep the enemy inside the bounds of a particular area
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            //Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }

    void CalcuateNewMovementVector()
    {
        // Create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * moveSpeed;
    }

    void InvokeIsWaiting()
    {
        latestDirectionChangeTime = Time.time;
        CalcuateNewMovementVector();
        isWaiting = false;
    }
}

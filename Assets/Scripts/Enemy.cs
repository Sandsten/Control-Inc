using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public Transform patrolPath;
    public bool loop = false; // If true will patrol in a loop, if false will move backwards through the patrol path after reaching the end
    private Vector3[] patrolPoints;
    private int currentPatrolPointIndex = 0;
    private bool movingBackwards = false;
    
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        patrolPoints = new Vector3[patrolPath.childCount];
        int i = 0;
        foreach (Transform patrolPoint in patrolPath) {
            patrolPoints[i] = new Vector3(patrolPoint.position.x, patrolPoint.position.y, patrolPoint.position.z);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = (patrolPoints[currentPatrolPointIndex] - transform.position).normalized;
        Debug.Log(moveDirection);
    }

    void FixedUpdate() 
    {
        
        rb.velocity = moveDirection * speed;

        //Debug.Log(rb.velocity);

        // Animate enemy
        if (moveDirection.x > 0) {
            animator.SetInteger("RunDirection", 2); // right
        } else if (moveDirection.x < 0) {
            animator.SetInteger("RunDirection", 4); // left
        } else if (moveDirection.y > 0) {
            animator.SetInteger("RunDirection", 1); // up
        } else if (moveDirection.y < 0) {
            animator.SetInteger("RunDirection", 3); // down
        } else {
            animator.SetInteger("RunDirection", 0); // idle
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "PatrolPoint") {
            if (loop) {
                if (currentPatrolPointIndex + 1 < patrolPoints.Length) {
                    currentPatrolPointIndex++;
                } else { // reached end
                    currentPatrolPointIndex = 0;
                }
            } else { // not looping
                if (movingBackwards) {
                    if (currentPatrolPointIndex - 1 >= 0) {
                        currentPatrolPointIndex--;
                    } else { //reached beginning
                        movingBackwards = false;
                        currentPatrolPointIndex++;
                    }
                } else { // moving forwards
                    if (currentPatrolPointIndex + 1 < patrolPoints.Length) {
                        currentPatrolPointIndex++;
                    } else { // reached end
                        movingBackwards = true;
                        currentPatrolPointIndex--;
                    }
                }
            }
        }
    }
}

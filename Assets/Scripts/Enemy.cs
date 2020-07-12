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

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public GameObject player;
    public GameManager gameManager;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        patrolPoints = new Vector3[patrolPath.childCount];
        int i = 0;
        foreach (Transform patrolPoint in patrolPath) {
            patrolPoints[i] = patrolPoint.position;//new Vector3(patrolPoint.position.x, patrolPoint.position.y, patrolPoint.position.z);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = patrolPoints[currentPatrolPointIndex] - transform.position;
        //Debug.Log(moveDirection);

        if(IsPlayerSpotted()){
            gameManager.playerHasBeenSpotted = true;
        }
    }

    void FixedUpdate() 
    {
        
        rb.velocity = Vector3.Normalize(moveDirection) * speed;
        // Debug.Log(rb.velocity);

        // Animate enemy
        if (moveDirection.x > 0 && Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y)) {
            animator.SetInteger("RunDirection", 2); // right
        } else if (moveDirection.x < 0 && Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y)) {
            animator.SetInteger("RunDirection", 4); // left
        } else if (moveDirection.y > 0 && Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x)) {
            animator.SetInteger("RunDirection", 1); // up
        } else if (moveDirection.y < 0 && Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x)) {
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

    public bool IsPlayerSpotted()
    {
        // Raycast in player direction 
        // Go to Edit > Project Settings > uncheck "Queries start in colliders" to prevent enemy from hitting themselves
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        float playerDistance = (player.transform.position - transform.position).magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection);
        // Is the player hit?
        if(hit.collider.tag == "Player") {
            // Is the angle to the player within the view angle?
            float angle = AngleToPlayer();
            // Is the player within the enemy's view radius?
            if(angle < viewAngle/2 && playerDistance <= viewRadius){
                // Debug.Log("Player spotted!!");
                return true;
            }
        }
        return false;
    }

    public float AngleToPlayer(){
        float angle = 0;

        Vector3 dirToPlayer = player.transform.position - gameObject.transform.position;
        angle = Vector3.Angle(moveDirection, dirToPlayer.normalized);

        return angle;
    }
}

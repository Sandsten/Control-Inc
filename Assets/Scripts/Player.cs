using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Animator animator;

    [Header("Control Mana")]
    public float maxMana = 20;
    public float manaConsumptionRate = 10f;
    public float manaForward;
    public float manaBackwards;
    public float manaRight;
    public float manaLeft;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start with max mana
        manaForward = manaBackwards = manaRight = manaLeft = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement inputs
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Animate player if moving
        if (moveDirection.x > 0 && manaRight > 0f) {
            animator.SetInteger("RunDirection", 2); // right
        } else if (moveDirection.x < 0 && manaLeft > 0f) {
            animator.SetInteger("RunDirection", 4); // left
        } else if (moveDirection.y > 0 && manaForward > 0f) {
            animator.SetInteger("RunDirection", 1); // up
        } else if (moveDirection.y < 0 && manaBackwards > 0f) {
            animator.SetInteger("RunDirection", 3); // down
        } else {
            animator.SetInteger("RunDirection", 0); // idle
        }

        // Consume mana based on move direction
        if (moveDirection.y > 0f)
            manaForward -= manaConsumptionRate * Time.deltaTime;
        if (moveDirection.y < 0f)
            manaBackwards -= manaConsumptionRate * Time.deltaTime;
        if (moveDirection.x > 0f)
            manaRight -= manaConsumptionRate * Time.deltaTime;
        if (moveDirection.x < 0f)
            manaLeft -= manaConsumptionRate * Time.deltaTime;

        // Clamp all the mana values
        manaForward = Mathf.Clamp(manaForward, 0f, maxMana);
        manaBackwards = Mathf.Clamp(manaBackwards, 0f, maxMana);
        manaRight = Mathf.Clamp(manaRight, 0f, maxMana);
        manaLeft = Mathf.Clamp(manaLeft, 0f, maxMana);

        // Prevent movement in the direction where mana == 0
        if(manaForward <= 0f && moveDirection.y > 0f) moveDirection.y = 0f;
        if(manaBackwards <= 0f && moveDirection.y < 0f) moveDirection.y = 0f;
        if(manaRight <= 0f && moveDirection.x > 0f) moveDirection.x = 0f;
        if(manaLeft <= 0f && moveDirection.x < 0f) moveDirection.x = 0f;

    }

    void FixedUpdate()
    {
        // Move player
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }



}

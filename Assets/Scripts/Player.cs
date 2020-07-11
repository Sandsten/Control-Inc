using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
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
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();

        // Start with max mana
        manaForward = manaBackwards = manaRight = manaLeft = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement inputs
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

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

    // Restores the given amount of mana to the provided mana type
    public void RestoreMana(ManaTypes type, float amount) {
        if(type == ManaTypes.MANA_FORWARD) manaForward += amount;
        if(type == ManaTypes.MANA_BACKWARDS) manaBackwards += amount;
        if(type == ManaTypes.MANA_RIGHT) manaRight += amount;
        if(type == ManaTypes.MANA_LEFT) manaLeft += amount;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Potion") {
            List<ManaTypes> potionType = other.gameObject.GetComponent<PotionStats>().potionType;
            float restoreAmmount = other.gameObject.GetComponent<PotionStats>().restoreAmount;
            
            foreach (ManaTypes manaType in potionType)
            {
                RestoreMana(manaType, restoreAmmount);
                Debug.Log("Mana restored!");
            }
            other.gameObject.SetActive(false);
        }

        if(other.tag == "Elevator"){
            //SceneManager.LoadScene("Level2");
            LevelManager.instance.ChangeLevel();
        }
    }

    void FixedUpdate()
    {
        // Move player
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

}

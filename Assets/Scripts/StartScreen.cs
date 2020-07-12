using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite oldSprite;

    void OnMouseOver(){
        spriteRenderer.sprite = newSprite; 
    }

    void OnMouseExit(){
        spriteRenderer.sprite = oldSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)){
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         }
    }
}

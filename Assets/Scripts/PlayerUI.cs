using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    private Text text;
    public Player player;

    [Header("Mana bars")]
    public Canvas manaBars;
    public Image manaUpBar;
    public Image manaDownBar;
    public Image manaRightBar;
    public Image manaLeftBar;

    public Color fullManaColor;
    public Color emptyManaColor;

    private Color defaultManaBarColor;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");  

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        DontDestroyOnLoad(canvasGO);
        canvasGO.name = "CanvasGO";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        DontDestroyOnLoad(textGO);
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        text = textGO.GetComponent<Text>();
        text.font = arial;
        text.text = "hello";
        text.fontSize =  20;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(-150, 90, 0);
        rectTransform.sizeDelta = new Vector2(600, 200);

        // int numberOfBars = manaBars.transform.childCount;
        DontDestroyOnLoad(manaBars);
        // for(int i = 0; i < numberOfBars; i++){
        //     GameObject o = manaBars.transform.GetChild(i).gameObject;
        //     o.transform.parent = canvas.transform;
        //     DontDestroyOnLoad(o);
        // }
        defaultManaBarColor = manaLeftBar.color;

    }

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        text.text = "Mana forward: " + Math.Round(GameObject.Find("Player").GetComponent<Player>().manaForward).ToString() + 
        " Mana backward: " + Math.Round(GameObject.Find("Player").GetComponent<Player>().manaBackwards).ToString() +
        " Mana right: " + Math.Round(GameObject.Find("Player").GetComponent<Player>().manaRight).ToString() +
        " Mana left: " + Math.Round(GameObject.Find("Player").GetComponent<Player>().manaLeft).ToString();

        UpdateManaBars();
    }


    void UpdateManaBars() 
    {   
        // ManaLeft
        float barWidthLeft = GetManaBarWidth(player.manaLeft);
        manaLeftBar.rectTransform.sizeDelta =  new Vector2(barWidthLeft, manaLeftBar.rectTransform.sizeDelta.y);
        manaLeftBar.color = GetManaBarColor(player.manaLeft);

        float barWidthRight = GetManaBarWidth(player.manaRight);
        manaRightBar.rectTransform.sizeDelta =  new Vector2(barWidthRight, manaRightBar.rectTransform.sizeDelta.y);
        manaRightBar.color = GetManaBarColor(player.manaRight);

        float barWidthUp = GetManaBarWidth(player.manaForward);
        manaUpBar.rectTransform.sizeDelta =  new Vector2(barWidthUp, manaUpBar.rectTransform.sizeDelta.y);
        manaUpBar.color = GetManaBarColor(player.manaForward);

        float barWidthDown = GetManaBarWidth(player.manaBackwards);
        manaDownBar.rectTransform.sizeDelta =  new Vector2(barWidthDown, manaDownBar.rectTransform.sizeDelta.y);
        manaDownBar.color = GetManaBarColor(player.manaBackwards);
    }

    float GetManaBarWidth(float remainingMana) 
    {
        float percentMana = remainingMana/player.maxMana;
        return Mathf.Lerp(10, 230, percentMana);
    }

    Color GetManaBarColor(float remainingMana)
    {
        float percentMana = remainingMana/player.maxMana;
        Color c = Color.Lerp(emptyManaColor, fullManaColor, percentMana);
        c.a = 1.0f;
        return c;
    }


    /* public void DisplayMana(float mf, float mb, float mr, float ml)
    {
        
    } */
}

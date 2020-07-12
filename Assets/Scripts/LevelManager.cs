using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool playerHasBeenSpotted = false; // Enemies will set this to true when they spot the player
    private bool loadingScene = false;
    private Vector3 spawnPoint = new Vector3(-6.91f, -4.21f, 0);

    public GameObject player;

    //public static LevelManager Instance { get { return instance; } }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadingScene = false;
        if (scene.name == "GameOver")
        {
            playerHasBeenSpotted = false;
            player.GetComponent<Player>().RestoreAllMana();
        }


        // Disable player UI & player when on the main menu
        if (scene.name == "StartMenue")
        {
            // playerUI.SetActive(false);
            // player.SetActive(false);
            PlayerUI.instance.gameObject.SetActive(false);
            // Player.instance.gameObject.SetActive(false);
        }
        else if (scene.name == "End"){
            PlayerUI.instance.gameObject.SetActive(false);
            // Player.instance.gameObject.SetActive(false);
        }
        else
        {
            PlayerUI.instance.gameObject.SetActive(true);
            Player.instance.gameObject.SetActive(true);
            // playerUI.SetActive(true);
            // player.SetActive(true);
        }

        // Move the player to the spawn point when a new scene loads
        player.transform.position = spawnPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Move the player to the spawn point on level 1
        player.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // Load the game over scene if the player has been spotted
        if (playerHasBeenSpotted && !loadingScene)
        {   
            Debug.Log("LOADING GAME OVER SCENE!");
            SceneManager.LoadScene("GameOver");
            loadingScene = true;
        }
    }

    public void ChangeLevel()
    {
        if(SceneManager.GetActiveScene().name == "Level3"){
            SceneManager.LoadScene(6);
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}

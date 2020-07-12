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
        }

        // Disable player UI & player when on the main menu
        if (scene.name == "StartMenue")
        {
            // playerUI.SetActive(false);
            // player.SetActive(false);
        }
        else
        {
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

    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool playerHasBeenSpotted = false; // Enemies will set this to true when they spot the player
    public GameObject playerUI;
    public GameObject player;
    private bool loadingScene = false;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadingScene = false;
        if(scene.name == "GameOver") {
            playerHasBeenSpotted = false;
        }

        // Disable player UI & player when on the main menu
        if(scene.name == "StartMenue"){
            playerUI.SetActive(false);
            player.SetActive(false);
        } else {
            playerUI.SetActive(true);
            player.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Load the game over scene if the player has been spotted
        if(playerHasBeenSpotted && !loadingScene) {
            SceneManager.LoadScene("GameOver");
            loadingScene = true;
        }
    }
}

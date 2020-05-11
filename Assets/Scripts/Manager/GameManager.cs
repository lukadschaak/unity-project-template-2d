using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InputType inputType;
    public float playerSpeed = 0.08f;
    public string currentSceneName;

    KeyCode nextLevelButton = KeyCode.Return;

    public GameObject mauzilla;

    void Awake()
    {
        // If instance doesn't exist, set to this 
        if (instance == null)
        {
            // Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        // If instance already exists and it's not this, destroy this (enforces
        // our singleton pattern, meaning there can only ever be one instance of a GameManager)
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Sets initially currentScenename. Should be "Start"
        currentSceneName = SceneManager.GetActiveScene().name;

        createPlayers();

        // activeSceneChanged is a Event that is fired, when SceneManager.LoadScene()
        // is called. Also fired multiple times on init.
        // The operation += adds a method that will be called when the event happens
        SceneManager.activeSceneChanged += changeCurrentSceneName;
    }

    void changeCurrentSceneName(Scene previousScene, Scene newScene)
    {
        // previousScene seems to be null all the time
        currentSceneName = newScene.name;
    }

    void Update()
    {
        checkSceneChange();
    }

    void createPlayers()
    {
        if (currentSceneName == "MainCity") {
            mauzilla = Instantiate(mauzilla, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            mauzilla.name = "Mauzilla";
        }
    }

    void checkSceneChange()
    {
        if (currentSceneName == "Start" && Input.GetKeyDown(nextLevelButton))
        {
            SceneManager.LoadScene("MainCity");
        }
        if (currentSceneName == "MainCity" && false)
        {
            SceneManager.LoadScene("xyz");
        }
        if (Input.GetKeyDown(nextLevelButton)
            && (currentSceneName == "ArtisansWins" || currentSceneName == "MauzillaWins")
        ) {
            SceneManager.LoadScene("Start");
        }
    }
}

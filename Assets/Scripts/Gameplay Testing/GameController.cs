using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    static public int score;
    static public int playerHealth;
    static public bool paused = false;
    static public bool kingHit = false;
    static public bool options = false;

    public static GameController Instance;

    private Scene scene;
    private GameObject UI;
    private GameObject pauseMenu;
    private GameObject optionsMenu;
    private GameObject HUD;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        if(scene.name == "LevelTesting")
        {

            UI = GameObject.Find("Canvases");

            HUD = UI.transform.GetChild(0).gameObject;
            pauseMenu = UI.transform.GetChild(1).gameObject;
            optionsMenu = UI.transform.GetChild(2).gameObject;

            if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
            {
                paused = true;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
            {
                paused = false;
            }

            if(kingHit)     
                GameWin();

            if (playerHealth <= 0)
                GameOver();

            if (paused && !options)
            {
                Time.timeScale = 0f;
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if(paused && options)
            {
                Time.timeScale = 0f;
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                optionsMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);
                options = false;
                HUD.SetActive(true);
                Time.timeScale = 1f;
            }
        }

        else if(scene.name == "MainMenu")
        {

        }

        else
        {
            if (Input.GetKey(KeyCode.R))
                SceneManager.LoadScene(0);
            if (Input.GetKey(KeyCode.Q))
                Application.Quit();
        }

    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    void GameWin()
    {
        SceneManager.LoadScene(3);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        playerHealth = 10;
        score = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

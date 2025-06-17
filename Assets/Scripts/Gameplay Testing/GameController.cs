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
    static public bool isGameOver = false;

    public static GameController Instance;

    private Scene scene;
    private GameObject UI;
    private GameObject pauseMenu;
    private GameObject optionsMenu;
    private GameObject HUD;
    private GameObject gameOverText;


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
            gameOverText = UI.transform.GetChild(3).gameObject;

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
                playerHealth += 5;

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
    }

    void GameOver()
    {
        HUD.SetActive(false);
        Time.timeScale = 0.2f;
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(3);
        gameOverText.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        loadMenu();
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

    public void loadMenu()
    {

        SceneManager.LoadScene(0);
    }

}

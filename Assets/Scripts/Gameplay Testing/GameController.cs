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

    public static GameController Instance;

    private Scene scene;

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

            if (Input.GetKey(KeyCode.Escape) && paused == false)
            {
                paused = true;
            }
            if (Input.GetKey(KeyCode.Escape) && paused == true)
            {
                paused = false;
            }

            if(kingHit)     
                GameWin();

            if (playerHealth <= 0)
                GameOver();
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
        Debug.Log(scene.name);
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

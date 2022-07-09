using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private PlayerController _playerController;

    // Use this for initialization
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape) && !ConsoleAdmin._showing)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }*/
    }

    public void Resume()
    {
        _playerController.canMove = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<PlayerInputManager>()._isPause = GameIsPaused;
    }

    public void Pause()
    {
        _playerController.canMove = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuV2", LoadSceneMode.Single);
        pauseMenuUI.SetActive(false);
        _playerController.canMove = false;

        MusicLevelController musicLevelController = GameObject.Find("SoundManager").GetComponent<MusicLevelController>();
        musicLevelController.act = -1;
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}

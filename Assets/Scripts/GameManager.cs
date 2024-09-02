using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _gameOver = false;

    public bool isCoOp = false;
    private bool _paused = false;
    private Animator _pauseAnim;

    [SerializeField]
    private GameObject _pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        _pauseAnim = _pauseMenu.GetComponent<Animator>();
        _pauseAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_paused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
            
        }
    }

    public void PauseGame()
    {
        _paused = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _pauseAnim.SetBool("isPaused", true);

    }

    public void ResumeGame()
    {
        _paused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _pauseAnim.SetBool("isPaused", false);
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}

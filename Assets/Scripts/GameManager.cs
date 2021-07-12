using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool winGame;

    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject titleScreen;

    public ParticleSystem[] fireworks;

    public Button restartButton;
    public Button mainMenuButton;

    public TextMeshProUGUI timerText;
    private float timerDecimalSecond;
    private int timerSecond;
    private int difficultyTimer;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timerDecimalSecond += 1 * Time.deltaTime;
            timerSecond = difficultyTimer - Mathf.RoundToInt(timerDecimalSecond);
            timerText.text = "Timer: " + timerSecond.ToString();
        }

        if (timerSecond < 0)
        {
            GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }

    public void WinGame()
    {
        isGameActive = false;
        winGame = true;
        winScreen.SetActive(true);

        StartCoroutine(waitFireworks());
        
    }

    IEnumerator waitFireworks()
    {
        while (winGame)
        {
            for (int i = 0; i < fireworks.Length; i++)
            {
                fireworks[i].Play();
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void StartGame(float difficulty, int difficultyTime)
    {
        difficultyTimer = difficultyTime;
        spawnManager.repeatDuration /= difficulty;

        isGameActive = true;
        winGame = false;

        StartCoroutine(spawnManager.SpawnTarget());
        titleScreen.SetActive(false);
    }
}

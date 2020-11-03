using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();    // Allows us to create events for other scripts to be notified
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;     //  Hold a static reference to this just in case we need it

    //  Create references to all of Cloud Hopper's pages
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject countDownMenu;
    public GameObject settingsMenu;
    public GameObject trophiesMenu;
    public Text scoreText;

    //  enum to keep track of what page we're on
    enum PageState {
        None,
        Start,
        GameOver,
        CountDown,
        Settings,
        Trophies
    }

    private int duringGameScore = 0;
    private bool gameOver = true;

    public bool getGameOver() {
        return this.gameOver;
    }

    void Awake() {
        Instance = this;
    }

    void OnEnable() {
        CountDownText.OnCountDownFinished += OnCountDownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
        scoreText.text = "";
    }

    void OnDisable() {
        CountDownText.OnCountDownFinished -= OnCountDownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountDownFinished() {
        setPageState(PageState.None);
        OnGameStarted();    // Call OnGameStarted event that is defined in GameManager class
        duringGameScore = 0;
        PlayerPrefs.SetInt("RunningScore", 0);
        gameOver = false;
        scoreText.text = "0";

    }

    void OnPlayerDied() {
        gameOver = true;
        int currentHighScore = PlayerPrefs.GetInt("HighScore");  //  Get current high score
        int totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
        totalDeaths++;
        PlayerPrefs.SetInt("TotalDeaths", totalDeaths);
        if (duringGameScore > currentHighScore) {
            PlayerPrefs.SetInt("HighScore", duringGameScore);
        }

        setPageState(PageState.GameOver);

    }

    void OnPlayerScored() {
        duringGameScore++;
        int currentTotalScore = PlayerPrefs.GetInt("TotalScore");
        currentTotalScore++;
        PlayerPrefs.SetInt("TotalScore", currentTotalScore);
        PlayerPrefs.SetInt("RunningScore", duringGameScore);
        scoreText.text = duringGameScore.ToString();
    }


    void setPageState(PageState state) {
        switch (state) {
            case PageState.None:
                startMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                countDownMenu.SetActive(false);
                settingsMenu.SetActive(false);
                trophiesMenu.SetActive(false);
                break;
            case PageState.Start:
                startMenu.SetActive(true);
                gameOverMenu.SetActive(false);
                countDownMenu.SetActive(false);
                settingsMenu.SetActive(false);
                trophiesMenu.SetActive(false);
                break;
            case PageState.GameOver:
                startMenu.SetActive(false);
                gameOverMenu.SetActive(true);
                countDownMenu.SetActive(false);
                settingsMenu.SetActive(false);
                trophiesMenu.SetActive(false);
                break;
            case PageState.CountDown:
                startMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                countDownMenu.SetActive(true);
                settingsMenu.SetActive(false);
                trophiesMenu.SetActive(false);
                break;
            case PageState.Settings:
                startMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                countDownMenu.SetActive(false);
                settingsMenu.SetActive(true);
                trophiesMenu.SetActive(false);
                break;
            case PageState.Trophies:
                startMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                countDownMenu.SetActive(false);
                settingsMenu.SetActive(false);
                trophiesMenu.SetActive(true);
                break;
        }
    }


    //  Activated when play button is clicked
    public void StartGame() {
        setPageState(PageState.CountDown);
    }


    // Activated when the achievements button is clicked
    public void OpenTrophies() {
        setPageState(PageState.Trophies);
    }

    public void ResetStats()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("TotalDeaths", 0);
    }

    //Activated when the back button in the trophies menu is clicked
    public void GoToStart() {
        setPageState(PageState.Start);
    }

    //  Activated when replay button is clicked
    public void ConfirmGameOver()
    {
        OnGameOverConfirmed();  //  event
        scoreText.text = "0";
        setPageState(PageState.CountDown);
    }

    public void ConfirmMainMenu()
    {
        OnGameOverConfirmed();
        setPageState(PageState.Start);
    }
}

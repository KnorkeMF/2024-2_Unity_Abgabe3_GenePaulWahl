using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("---Verknüpfungen----")]
    [SerializeField] public PlayerController player;
    [SerializeField] public CoinManager coinManager;
    
    [Header("---Counter---")]
    [SerializeField] public TextMeshProUGUI coinCounterText;
    [SerializeField] public GameObject panelCoinCounter;
    [SerializeField] public TextMeshProUGUI diaCounterText;
    [SerializeField] public GameObject panelDiaCounter;
   
    
    [Header("---Countdown---")]
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] public GameObject panelCountdown;
    bool isGameOver = false;
    private int countdownValue;
    
    [Header("---GameLost---")]
    [SerializeField] private GameObject panelLost;
    [SerializeField] Button buttonRestart;
    [SerializeField] Button buttonMainMenuLost;
    [SerializeField] public TextMeshProUGUI countdownTextLost;
    
    [Header("---GameWon---")]
    [SerializeField] GameObject panelWon;
    [SerializeField] Button buttonContinue;
    [SerializeField] Button buttonMainMenuWon;
    [SerializeField] public TextMeshProUGUI countdownTextWin;
    
    [Header("---Score---")]
    [SerializeField] public TextMeshProUGUI scoreTextWin;
    

    private void Start()
    {
        panelCoinCounter.SetActive(true);
        panelLost.SetActive(false);
        panelWon.SetActive(false);
        
        
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonContinue.onClick.AddListener(LoadNextLevel);
        buttonMainMenuLost.onClick.AddListener(LoadMainMenu);
        buttonMainMenuWon.onClick.AddListener(LoadMainMenu);

        StartCoroutine(Countdown());
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void UpdateCoinText(int newCoinCount)
    {
        coinCounterText.text = newCoinCount.ToString(); 
    }

    public void UpdateDiaText(int newDiaCount)
    {
        diaCounterText.text = newDiaCount.ToString();
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
        isGameOver = true;
    }

    public void ShowPanelWin()
    {
        UpdateScoreText();
        Debug.Log("Panel Win");
        panelWon.SetActive(true);
        isGameOver = true;
        player.canMove = false;
        player.rb.linearVelocity = Vector2.zero;
      
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    
    IEnumerator Countdown()
    {
        countdownValue = 20;
        while (countdownValue >= 0 && !isGameOver)
        {
            Debug.Log("countdown = " + countdownValue);
            UpdateCountdownText();
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        Debug.Log("countdown end");
    }


    void UpdateCountdownText()
    {
        countdownText.text = countdownValue.ToString();
        countdownTextWin.text = countdownValue.ToString();
        countdownTextLost.text = countdownValue.ToString();
        Debug.Log(message:"countdown updated");

        if (countdownValue <= 0)
        {
            ShowPanelLost();
            player.MovementStop();
        }
    }


    void UpdateScoreText()
    {
        int finalScore = countdownValue * 100 + coinManager.coinCounter * 15 + coinManager.diaCounter * 50;
        scoreTextWin.text = "Score =" + finalScore.ToString();
    }

    public void PlusCountdown()
    {
        countdownValue = countdownValue + 5;
    }

    public void PlusCountdownSmall()
    {
        countdownValue = countdownValue + 1;    
    }
}
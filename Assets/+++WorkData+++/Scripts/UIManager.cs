using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    [SerializeField] public TextMeshProUGUI counterText;
    [SerializeField] public GameObject panelCounter;
    
    [SerializeField] private GameObject panelLost;
    [SerializeField] Button buttonRestart;
    [SerializeField] Button buttonMainMenuLost;
    
    [SerializeField] GameObject panelWon;
    [SerializeField] Button buttonRestartWon;
    [SerializeField] Button buttonMainMenuWon;

    private void Start()
    {
        panelCounter.SetActive(true);
        panelLost.SetActive(false);
        panelWon.SetActive(false);
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonRestartWon.onClick.AddListener(RestartLevel);
        buttonMainMenuLost.onClick.AddListener(LoadMainMenu);
        buttonMainMenuWon.onClick.AddListener(LoadMainMenu);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void UpdateCoinText(int newCoinCount)
    {
        counterText.text = newCoinCount + "/5"; 
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void ShowPanelWin()
    {
        Debug.Log("Panel Win");
        panelWon.SetActive(true);
        
    }

}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("---BUttons---")]
    [SerializeField] Button buttonStartGame;
    [SerializeField] Button buttonBack;
    [SerializeField] Button buttonLevelSelect;
    [SerializeField] Button buttonLevelTutorial;
    [SerializeField] Button buttonLevel1;
  
    [Header("---Panels---")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;
    
    void Start()
    {
        
        levelSelectPanel.SetActive(false);
        
        //Button Functions
        buttonStartGame.onClick.AddListener(StartGame);
        buttonLevelSelect.onClick.AddListener(LevelSelectPanel);
        buttonBack.onClick.AddListener(Back);
        buttonLevelTutorial.onClick.AddListener(StartGame);
        buttonLevel1.onClick.AddListener(StartLevel1);
    }


    void StartGame()
    {
        Debug.Log("Startet Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void LevelSelectPanel()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    void Back()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    void StartLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

}

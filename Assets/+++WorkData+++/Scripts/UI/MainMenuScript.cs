using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] Button buttonStartGame;
    [SerializeField] Button buttonBack;
    [SerializeField] private GameObject mainMenuPanel;
    
    void Start()
    {
        
        //Button Functions
        buttonStartGame.onClick.AddListener(StartGame);
    }


    void StartGame()
    {
        Debug.Log("Startet Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

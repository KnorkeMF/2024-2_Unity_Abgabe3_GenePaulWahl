using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.05f;
    [TextArea(3, 10)]
    public string startMessage;
    private Coroutine typingCoroutine;
    public AudioSource audioSource;
    
    public GameObject nextButton;

    private void Start()
    {
        nextButton.SetActive(false);
        ShowDialog(startMessage);
    }
    public void ShowDialog(string message)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        dialogText.text = "";
        int letterCount = 0;
        
        foreach (char letter in message.ToCharArray())
        {
            dialogText.text += letter;
            letterCount++;

            int playSoundPerLetter = 4;

            if (letterCount % playSoundPerLetter == 0)
            {
                if (char.IsLetterOrDigit(letter))
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
           
            yield return new WaitForSeconds(typingSpeed);
        }
        
        nextButton.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


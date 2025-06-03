using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

[SerializeField] public int counter = 0;
[SerializeField] private UIManager uiManager;


    private void Start()
    {
      counter = 0;
      uiManager.UpdateCoinText(counter);
    }

    public void AddCoin()
    {
        counter++;
        uiManager.UpdateCoinText(counter);
    }
    
}

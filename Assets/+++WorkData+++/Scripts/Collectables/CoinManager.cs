using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

[SerializeField] public int coinCounter = 0;
[SerializeField] public int diaCounter = 0;
[SerializeField] private UIManager uiManager;


    private void Start()
    {
      coinCounter = 0;
      uiManager.UpdateCoinText(coinCounter);
      diaCounter = 0;
      uiManager.UpdateDiaText(diaCounter);
    }

    public void AddCoin()
    {
        coinCounter++;
        uiManager.UpdateCoinText(coinCounter);
        uiManager.PlusCountdownSmall();
    }

    public void AddDia()
    {
        diaCounter++;
        uiManager.UpdateDiaText(diaCounter);
    }
    
}

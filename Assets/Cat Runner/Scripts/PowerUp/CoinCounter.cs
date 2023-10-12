using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public Text coinText; 

    void Update()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinText.text = "Coins: " + coinCount.ToString();
    }
}

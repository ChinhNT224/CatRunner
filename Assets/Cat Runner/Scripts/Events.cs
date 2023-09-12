using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Text coinText;
    public void ReplayGame() {
        SceneManager.LoadScene("Play");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinText.text = "Coins: " + coinCount.ToString();
    }
}

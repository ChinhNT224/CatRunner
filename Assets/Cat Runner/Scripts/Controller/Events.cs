using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Text coinText;
    public Text scoreText;
    public GameObject popUpMenu;
    public GameObject musicSetting;

    public void PlayGame()
    {
        SceneManager.LoadScene("Play");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateS()
    {
        SceneManager.LoadScene("Update");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PopUpMenu()
    {
        if (popUpMenu != null)
        {
            popUpMenu.SetActive(!popUpMenu.activeSelf); 
        }
    }

    public void MusicSetting()
    {
        if (musicSetting != null)
        {
            musicSetting.SetActive(!musicSetting.activeSelf);
        }
    }

    public void CloseMusicSetting()
    {
        if (musicSetting != null)
        {
            musicSetting.SetActive(false);
        }
    }

    public void IncreaseCoins()
    {
        GameObject AdRewardADS = GameObject.Find("AdReward");
        RewardedAdsButton rewardedAdsButton = AdRewardADS.GetComponent<RewardedAdsButton>();
        if (rewardedAdsButton != null)
        {
            rewardedAdsButton.LoadAd();
            rewardedAdsButton.ShowAd();
        }
        else
        {
            Debug.LogError("Không tìm thấy RewardedAdsButton.");
        }
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinCount += 100; 
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
    }


    void Update()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinText.text = coinCount.ToString();
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        scoreText.text = playerScore.ToString();
    }
}

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject GameOver;
    public GameObject[] characterPrefabs;
    public GameObject pauseSetting;
    private bool hasRespawned = false;
    public Button ReviceButton;


    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject go = Instantiate(characterPrefabs[index], transform.position, Quaternion.identity);
    }

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        PlayerController.isColliding = false;
        AudioManager.instance.SetBackgroundMusicType(AudioManager.BackgroundMusicType.Menu);
    }

    public void PauseGame()
    {
        if (pauseSetting != null)
        {
            pauseSetting.SetActive(!pauseSetting.activeSelf);
        }
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (pauseSetting != null)
        {
            pauseSetting.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Play");
    }

    public void OnRespawnButtonClicked()
    {
        if (!hasRespawned)
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
            Time.timeScale = 1;
            gameOver = false;
            PlayerController.isColliding = false;
            if (GameOver != null)
            {
                GameOver.SetActive(false);
            }

            hasRespawned = true;
        }
        else
        {
            Debug.Log("Người chơi đã hồi sinh rồi.");
            ReviceButton.interactable = false;
        }
    }
}

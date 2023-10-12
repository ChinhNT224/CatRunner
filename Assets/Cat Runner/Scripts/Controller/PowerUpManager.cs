using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public Slider doubleCoinSlider;
    public Slider magnetSlider;
    public Text doubleCoinCostText;
    public Text magnetCostText;

    private int doubleCoinLevel = 1;
    private int magnetLevel = 1;

    private float[] doubleCoinDurations = { 10f, 15f, 20f, 25f, 30f };
    private float[] magnetDurations = { 10f, 15f, 20f, 25f, 30f };

    private int[] upgradeCosts = { 100, 500, 1000, 1500, 2000 };

    private void Start()
    {
        doubleCoinLevel = PlayerPrefs.GetInt("DoubleCoinLevel", 1);
        magnetLevel = PlayerPrefs.GetInt("MagnetLevel", 1);

        UpdateSliders();
        UpdateCostText();
    }

    private void UpdateSliders()
    {
        doubleCoinSlider.value = (float)doubleCoinLevel / upgradeCosts.Length;
        magnetSlider.value = (float)magnetLevel / upgradeCosts.Length;
    }

    private void UpdateCostText()
    {
        doubleCoinCostText.text = doubleCoinLevel < upgradeCosts.Length ? upgradeCosts[doubleCoinLevel - 1].ToString() : "Max";
        magnetCostText.text = magnetLevel < upgradeCosts.Length ? upgradeCosts[magnetLevel - 1].ToString() : "Max";
    }

    public void UpgradeDoubleCoin()
    {
        if (doubleCoinLevel < upgradeCosts.Length)
        {
            int cost = upgradeCosts[doubleCoinLevel - 1];
            int coinCount = PlayerPrefs.GetInt("CoinCount", 0);

            if (coinCount >= cost)
            {
                coinCount -= cost;
                doubleCoinLevel++;
                PlayerPrefs.SetInt("CoinCount", coinCount);
                PlayerPrefs.SetInt("DoubleCoinLevel", doubleCoinLevel);
                PlayerPrefs.SetFloat("DoubleCoinDuration", doubleCoinDurations[doubleCoinLevel - 1]);
                PlayerPrefs.Save();
                UpdateSliders();
                UpdateCostText();
            }
            else
            {
                Debug.Log("Không đủ tiền để nâng cấp Double Coin.");
            }
        }
        else
        {
            Debug.Log("Bạn đã đạt mức nâng cấp tối đa cho Double Coin.");
        }
    }

    public void UpgradeMagnet()
    {
        if (magnetLevel < upgradeCosts.Length)
        {
            int cost = upgradeCosts[magnetLevel - 1];
            int coinCount = PlayerPrefs.GetInt("CoinCount", 0);

            if (coinCount >= cost)
            {
                coinCount -= cost;
                magnetLevel++;
                PlayerPrefs.SetInt("CoinCount", coinCount);
                PlayerPrefs.SetInt("MagnetLevel", magnetLevel);
                PlayerPrefs.SetFloat("MagnetDuration", magnetDurations[magnetLevel - 1]);
                PlayerPrefs.Save();
                UpdateSliders();
                UpdateCostText();
            }
            else
            {
                Debug.Log("Không đủ tiền để nâng cấp Magnet.");
            }
        }
        else
        {
            Debug.Log("Bạn đã đạt mức nâng cấp tối đa cho Magnet.");
        }
    }
}

using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopElement[] characters;

    public int characterIndex;
    public GameObject[] shopCharacters;

    public Button buyButton;
    public Button selectedButton;

    void Start()
    {
        foreach (ShopElement c in characters)
        {
            if (c.price != 0)
                c.isLocked = PlayerPrefs.GetInt(c.name, 1) == 1 ? true : false;
        }

        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject ch in shopCharacters)
        {
            ch.SetActive(false);
        }

        shopCharacters[characterIndex].SetActive(true);

        Time.timeScale = 1;

        UpdateUI();
    }


    public void ChangeNextCharacter()
    {
        shopCharacters[characterIndex].SetActive(false);

        characterIndex++;
        if (characterIndex == characters.Length)
            characterIndex = 0;

        shopCharacters[characterIndex].SetActive(true);

        UpdateUI();

        bool isLocked = characters[characterIndex].isLocked;
        if (isLocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
    }



    public void ChangePreviousCharacter()
    {
        shopCharacters[characterIndex].SetActive(false);

        characterIndex--;
        if (characterIndex == -1)
            characterIndex = characters.Length - 1;

        shopCharacters[characterIndex].SetActive(true);

        UpdateUI();

        bool isLocked = characters[characterIndex].isLocked;
        if (isLocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
    }

    public void UnlockWithGems()
    {
        AudioManager.instance.UnlockSound();
        ShopElement c = characters[characterIndex];
        if (PlayerPrefs.GetInt("CoinCount", 0) < c.price)
            return;

        int newGems = PlayerPrefs.GetInt("CoinCount", 0) - characters[characterIndex].price;
        PlayerPrefs.SetInt("CoinCount", newGems);

        c.isLocked = false;
        PlayerPrefs.SetInt(c.name, 0);
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);

        UpdateUI();
    }

    private void UpdateUI()
    {
        ShopElement c = characters[characterIndex];

        if (c.isLocked)
        {
            selectedButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<Text>().text = c.price + "";

            if (PlayerPrefs.GetInt("CoinCount", 0) < c.price)
                buyButton.interactable = false;
            else
                buyButton.interactable = true;
        }
        else
        {
            buyButton?.gameObject.SetActive(false);
            selectedButton?.gameObject.SetActive(true);
        }

    }
}

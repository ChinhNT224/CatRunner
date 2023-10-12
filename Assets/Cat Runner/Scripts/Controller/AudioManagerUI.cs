using UnityEngine;
using UnityEngine.UI;

public class AudioManagerUI : MonoBehaviour
{
    public Button musicToggleButton;
    public Button soundToggleButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        UpdateButtonSprites();

        musicToggleButton.onClick.AddListener(ToggleMusic);
        soundToggleButton.onClick.AddListener(ToggleSound);
    }

    private void UpdateButtonSprites()
    {
        musicToggleButton.image.sprite = IsMusicOn() ? musicOnSprite : musicOffSprite;
        soundToggleButton.image.sprite = IsSoundOn() ? soundOnSprite : soundOffSprite;
    }

    public void ToggleMusic()
    {
        bool isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
        PlayerPrefs.SetInt("IsMusicOn", isMusicOn ? 0 : 1);
        PlayerPrefs.Save();

        musicToggleButton.image.sprite = IsMusicOn() ? musicOnSprite : musicOffSprite;
    }

    public void ToggleSound()
    {
        bool isSoundOn = PlayerPrefs.GetInt("IsSoundOn", 1) == 1;
        PlayerPrefs.SetInt("IsSoundOn", isSoundOn ? 0 : 1);
        PlayerPrefs.Save();

        soundToggleButton.image.sprite = IsSoundOn() ? soundOnSprite : soundOffSprite;
    }

    public bool IsMusicOn()
    {
        return PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
    }

    public bool IsSoundOn()
    {
        return PlayerPrefs.GetInt("IsSoundOn", 1) == 1;
    }
}

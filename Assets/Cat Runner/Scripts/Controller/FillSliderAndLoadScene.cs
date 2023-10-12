using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FillSliderAndLoadScene : MonoBehaviour
{
    public Slider fillSlider;
    public float fillDuration = 2.0f; 
    public string menuSceneName = "Menu"; 

    private bool isFilling = false;
    private float fillStartTime;

    private void Start()
    {
        fillSlider.value = 0f;
        isFilling = true;
        fillStartTime = Time.time;
    }

    private void Update()
    {
        if (isFilling)
        {
            float elapsedTime = Time.time - fillStartTime;
            float fillAmount = elapsedTime / fillDuration;
 
            fillAmount = Mathf.Clamp01(fillAmount);
            Debug.Log(fillAmount);

            fillSlider.value = fillAmount;

            if (fillAmount >= 1.0f)
            {
                SceneManager.LoadScene(menuSceneName);
            }
        }
    }
}

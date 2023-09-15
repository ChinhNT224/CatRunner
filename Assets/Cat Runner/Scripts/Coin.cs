using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 5f;
    public int coinValue = 1;

    CoinMove coinMoveScript;
    private bool isCollected = false;
    private bool isDoubleActive = false;

    public GameObject coinModel;
    public GameObject doubleCoinModel;

    private static List<Coin> allCoins = new List<Coin>(); 
    private static bool isGlobalDoubleActive = false; 

    private void Awake()
    {
        allCoins.Add(this);
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            Debug.Log($"Va chạm2: {other.name}");
            isCollected = true;
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                int scoreToAdd = isDoubleActive ? coinValue * 2 : coinValue;
                playerController.IncreaseCoins(scoreToAdd);
            }
            gameObject.SetActive(false);
        }

        if (!isCollected && other.CompareTag("Coin Detector"))
        {
            coinMoveScript.enabled = true;
        }
    }

    private void Update()
    {
        if (isDoubleActive)
        {
            ActivateDoubleCoin(10f);
        }
    }

    public void OnDoubleCoinCollected(float duration)
    {
        isDoubleActive = true;
        ChangeAllCoinsToDouble(); 
    }

    private void ChangeAllCoinsToDouble()
    {
        foreach (Coin coin in allCoins)
        {
            if (coin != null && coin.gameObject.activeSelf)
            {
                coin.ActivateDoubleCoin(10f); 
            }
        }
    }

    public void ActivateDoubleCoin(float duration)
    {
        isDoubleActive = true;
        doubleCoinModel.SetActive(true);
        coinModel.SetActive(false);

        if (duration > 0f)
        {
            StartCoroutine(DeactivateDoubleCoinAfterDuration(duration));
        }
    }

    private IEnumerator DeactivateDoubleCoinAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDoubleActive = false;
        doubleCoinModel.SetActive(false);
        coinModel.SetActive(true);
    }
}
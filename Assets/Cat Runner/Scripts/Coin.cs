using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 5f;

    CoinMove coinMoveScript;
    public int coinValue = 1;
    private bool isCollected = false;
    private bool isDoubleActive = false;

    public GameObject coinModel;
    public GameObject doubleCoinModel;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            Debug.Log($"va chạm2: {other.name}");
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
            Debug.Log($"va chạm1: {other.name}");
            coinMoveScript.enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDoubleActive = true;
            doubleCoinModel.SetActive(true);
            coinModel.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isDoubleActive = false;
            doubleCoinModel.SetActive(false);
            coinModel.SetActive(true); 
        }
    }
}

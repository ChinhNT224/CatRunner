using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public CoinDetector coinDetector;
    public float activationDuration = 10f;

    void Start()
    {
        coinDetector = GameObject.FindGameObjectWithTag("Coin Detector").GetComponent<CoinDetector>();
        coinDetector.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            coinDetector.Activate(activationDuration);
            Destroy(gameObject);
        }
    }
}

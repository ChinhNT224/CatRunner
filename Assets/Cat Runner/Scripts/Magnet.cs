using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float activationDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transform coinDetectorTransform = other.transform.Find("Coin Detector");

            if (coinDetectorTransform != null)
            {
                GameObject coinDetectorObject = coinDetectorTransform.gameObject;

                if (!coinDetectorObject.activeInHierarchy)
                {
                    CoinDetector coinDetector = coinDetectorObject.GetComponent<CoinDetector>();
                    coinDetector.Activate(activationDuration);
                }
            }
            else
            {
                Debug.Log("No child with name 'Coin Detector' found.");
            }

            Destroy(gameObject);
        }
    }
}

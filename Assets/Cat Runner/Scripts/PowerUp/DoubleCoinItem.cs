using UnityEngine;

public class DoubleCoinItem : MonoBehaviour
{
    public float doubleDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject coinObject = GameObject.FindGameObjectWithTag("Coin");

            if (coinObject != null)
            {
                Coin coinScript = coinObject.GetComponent<Coin>();
                if (coinScript != null)
                {
                    coinScript.OnDoubleCoinCollected(doubleDuration);
                }
            }

            Destroy(gameObject);
        }
    }
}

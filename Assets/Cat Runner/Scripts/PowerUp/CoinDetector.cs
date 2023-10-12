using UnityEngine;
using System.Collections;

public class CoinDetector : MonoBehaviour
{

    public void Activate(float duration)
    {
        gameObject.SetActive(true);
        StartCoroutine(DeactivateAfterDuration(duration));
    }

    private IEnumerator DeactivateAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

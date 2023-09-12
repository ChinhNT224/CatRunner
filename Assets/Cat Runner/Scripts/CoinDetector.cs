using UnityEngine;
using System.Collections;

public class CoinDetector : MonoBehaviour
{
    private bool isActive = false;

    public void Activate(float duration)
    {
        isActive = true;
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
        isActive = false;
        gameObject.SetActive(false);
    }
}

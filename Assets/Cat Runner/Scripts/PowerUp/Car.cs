using UnityEngine;

public class Car : MonoBehaviour
{
    public float moveSpeed = -10f;
    public float activationDistance = 30f; 

    private Transform playerTransform;
    private bool isRunning = false;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < activationDistance)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning)
        {
            Transform myTransform = transform;
            myTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform target;
    private Vector3 offset;

    void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        target = player.transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(transform.position.x, offset.y + target.position.y, offset.z + target.position.z);
            transform.position = newPosition;
        }
    }
}
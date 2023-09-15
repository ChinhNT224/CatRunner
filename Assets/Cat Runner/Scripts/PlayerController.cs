using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Animator animator;
    public float forwardSpeed = 5f;
    private int desiredLane = 1;
    public float laneDistance = 2f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    private bool isColliding = false;
    private bool isRolling = false;
    private float rollDuration = 1.0f;
    private float rollTimer = 0.0f;
    private int coinCount = 0;
    private float laneChangeTimer = 0.0f;
    public float laneChangeDuration = 0.01f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(0f, rb.velocity.y, forwardSpeed);

        if (!isColliding)
        {
            moveDirection.y += gravity * Time.deltaTime;
            rb.velocity = moveDirection;
        }

        if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            if (SwipeManager.swipeUp)
            {
                animator.SetTrigger("Jump");
                Jump();
            }
        }

        if (!isColliding && !isRolling)
        {
            if (laneChangeTimer < laneChangeDuration)
            {
                laneChangeTimer += Time.deltaTime;
                float t = laneChangeTimer / laneChangeDuration;

                // Ánh xạ tuyến tính từ vị trí ban đầu đến vị trí đích
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            }

            if (SwipeManager.swipeRight)
            {
                if (desiredLane < 2 && laneChangeTimer >= laneChangeDuration)
                {
                    animator.SetTrigger("RunRight");
                    desiredLane++;
                    laneChangeTimer = 0.0f; // Đặt lại thời gian chuyển làn
                    initialPosition = transform.position; // Cập nhật vị trí ban đầu
                    targetPosition = initialPosition + Vector3.right * laneDistance; // Tính toán vị trí đích
                }
            }

            if (SwipeManager.swipeLeft)
            {
                if (desiredLane > 0 && laneChangeTimer >= laneChangeDuration)
                {
                    animator.SetTrigger("RunLeft");
                    desiredLane--;
                    laneChangeTimer = 0.0f; // Đặt lại thời gian chuyển làn
                    initialPosition = transform.position; // Cập nhật vị trí ban đầu
                    targetPosition = initialPosition - Vector3.right * laneDistance; // Tính toán vị trí đích
                }
            }

            if (SwipeManager.swipeDown)
            {
                animator.SetTrigger("Roll");
                StartRoll();
            }
        }

        if (isRolling)
        {
            rollTimer += Time.deltaTime;

            if (rollTimer >= rollDuration)
            {
                StopRoll();
            }
        }
    }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void StartRoll()
    {
        isRolling = true;
        playerCollider.height = 5f;
        playerCollider.center = new Vector3(0f, playerCollider.height / 2f, 0f);
        rollTimer = 0.0f;
    }

    private void StopRoll()
    {
        if (isRolling)
        {
            isRolling = false;
            playerCollider.height = 11.0f;
            playerCollider.center = new Vector3(0f, 4.849356f, -0.308789f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"va chạm: {other.name}");
        if (other.CompareTag("HitBarrier"))
        {
            isColliding = true;
            PlayerManager.gameOver = true;
        }
    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
    }
}

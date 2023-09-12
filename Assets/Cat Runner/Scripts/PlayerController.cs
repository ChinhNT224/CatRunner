using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Animator animator;
    public float forwardSpeed;
    private int desiredLane = 1;
    public float laneDistance = 2f;
    public float jumpForce;
    public float gravity = -20f;
    private bool isColliding = false;
    private bool isRolling = false;
    private float rollDuration = 1.0f; 
    private float rollTimer = 0.0f;
    private int coinCount = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(0f, rb.velocity.y, forwardSpeed);

        if (!isColliding)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        rb.velocity = moveDirection;

        if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator.SetTrigger("Jump");
                Jump();               
            }
        }

        if (!isColliding && !isRolling)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetTrigger("RunRight");
                desiredLane++;
                if (desiredLane == 3)
                {
                    desiredLane = 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                animator.SetTrigger("RunLeft");
                desiredLane--;
                if (desiredLane == -1)
                {
                    desiredLane = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
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

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = targetPosition;
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
            Debug.LogError("va chạm");
        }
    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount); 
        PlayerPrefs.Save(); 
    }


}

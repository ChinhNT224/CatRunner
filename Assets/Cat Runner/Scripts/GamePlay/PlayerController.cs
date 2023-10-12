using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Animator animator;
    public float forwardSpeed = 8f;
    private int desiredLane = 1;
    public float laneDistance = 2f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    public static bool isColliding = false;
    private bool isRolling = false;
    private float rollDuration = 1.0f;
    private float rollTimer = 0.0f;
    private int coinCount = 0;
    private float laneChangeTimer = 0.0f;
    public float laneChangeDuration = 0.1f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private int playerScore = 0; 
    private float scoreUpdateInterval = 0.1f; 
    private float scoreTimer = 0.0f;
    private bool canMove = true;
    private Vector3 currentLvPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        initialPosition = transform.position;
        targetPosition = initialPosition;
        AudioManager.instance.SetBackgroundMusicType(AudioManager.BackgroundMusicType.Play);
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
                AudioManager.instance.JumpSound();
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

                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            }

            if (!canMove)
            {
                return;
            }
            else
            {
                if (SwipeManager.swipeRight)
                {
                    if (desiredLane < 2 && laneChangeTimer >= laneChangeDuration)
                    {
                        animator.SetTrigger("RunRight");
                        desiredLane++;
                        laneChangeTimer = 0.0f;
                        initialPosition = transform.position;
                        targetPosition = initialPosition + Vector3.right * laneDistance;
                    }
                }

                if (SwipeManager.swipeLeft)
                {
                    if (desiredLane > 0 && laneChangeTimer >= laneChangeDuration)
                    {
                        animator.SetTrigger("RunLeft");
                        desiredLane--;
                        laneChangeTimer = 0.0f;
                        initialPosition = transform.position;
                        targetPosition = initialPosition - Vector3.right * laneDistance;
                    }
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

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreUpdateInterval)
        {
            UpdateScore();
            scoreTimer = 0.0f;
        }
    }

    private void UpdateScore()
    {
        playerScore += 1;

        int currentPlayerScore = PlayerPrefs.GetInt("PlayerScore", 0);

        if (playerScore > currentPlayerScore)
        {
            PlayerPrefs.SetInt("PlayerScore", playerScore);
            PlayerPrefs.Save();
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
            AudioManager.instance.DeathSound();
            Destroy(other.gameObject);

            isColliding = true;
            PlayerManager.gameOver = true;
        }
        if (other.CompareTag("OneWay"))
        {
            canMove = false;
        }
        if (other.CompareTag("Portal1"))
        {
            GameObject LvM = GameObject.Find("LvManager");
            LvManager lvManager = LvM.GetComponent<LvManager>();
            lvManager.Portal1(currentLvPosition);

            Vector3 newPosition = transform.position;
            newPosition.z = 1;
            transform.position = newPosition;
        }
        if (other.CompareTag("Portal2"))
        {
            GameObject LvM = GameObject.Find("LvManager");
            LvManager lvManager = LvM.GetComponent<LvManager>();
            lvManager.Portal2(currentLvPosition);

            Vector3 newPosition = transform.position;
            newPosition.z = 1;
            transform.position = newPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OneWay"))
        {
            canMove = true; 
        }
    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
    }
}

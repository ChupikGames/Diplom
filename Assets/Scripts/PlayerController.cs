using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Animator anim;
    private Vector3 dir;
    [SerializeField] private float speed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private int maxScore;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private Text inGameCoinsText;
    [SerializeField] private Text resultCoinsText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private float slideAnimTime;
    [SerializeField] private float jumpAnimTime;
    private Score score;
    public float time = 1;
    private int inGameCoins;

    private int lineToMove = 1; //дорожка на которой мы сейчас
    public float lineDistance = 4; // расстояние между дорожек

    private bool isSliding = false;
    private bool isJumping = false;
    private bool inGame = false;





    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = time;
        startPanel.SetActive(true);


        

        maxScore = PlayerPrefs.GetInt("MaxScore");
        maxScoreText.text = maxScore.ToString();

        score = GetComponent<Score>();

    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = coins.ToString();
        if (inGame)
        {
            if (SwipeController.swipeRight)
            {
                if (lineToMove < 2)
                    lineToMove++;
            }
            if (SwipeController.swipeLeft)
            {
                if (lineToMove > 0)
                    lineToMove--;
            }

            if (SwipeController.swipeUp)
            {
                if (controller.isGrounded)
                    StartCoroutine(Jump());
            }

            if (SwipeController.swipeDown)
            {
                StartCoroutine(Slide());
            }
        }

        

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPos += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPos += Vector3.right * lineDistance;

        if (transform.position == targetPos)
            return;
        Vector3 diff = targetPos - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }

    private IEnumerator Jump()
    {
        dir.y = jumpForce;
        gravity = -30f;
        controller.center = new Vector3(0, 1.56f, 0);
        controller.height = 2.67f;
        isJumping = true;
        anim.Play("Jumping");
        
        yield return new WaitForSeconds(jumpAnimTime);
        if (!isSliding)
        {
            controller.center = new Vector3(0, 0.35f, 0);
            controller.height = 2.67f;
            isJumping = false;
        }
        else
            isJumping = false;
        gravity = -10f;

    }

    private IEnumerator Slide()
    {
        dir.y -= jumpForce;
        controller.center = new Vector3(0, -0.265f, 0.7f);
        controller.height = 1.45f;
        isSliding = true;
        anim.Play("Slide");


        yield return new WaitForSeconds(slideAnimTime);

        if (!isJumping)
        {
            controller.center = new Vector3(0, 0.35f, 0);
            controller.height = 2.67f;
            isSliding = false;

        }
        else
        {
            isSliding = false;
        }
        



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Obstacle")
        {
            inGame = false;
            anim.Play("Death_anim");
            speed = 0;
            coins += inGameCoins;
            PlayerPrefs.SetInt("Coins", coins);
            resultCoinsText.text = inGameCoins.ToString();
            inGamePanel.SetActive(false);
            losePanel.SetActive(true);

            if (maxScore < score.score)
            {
                maxScore = score.score;
                PlayerPrefs.SetInt("MaxScore", maxScore);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            inGameCoins++;
            Destroy(other.gameObject);
            inGameCoinsText.text = inGameCoins.ToString();
        }
    }

    public IEnumerator start_running()
    {
        inGame = true;
        startPanel.SetActive(false);
        inGamePanel.SetActive(true);

        anim.Play("Crouch before running");
        yield return new WaitForSeconds(1);
        speed = 15;
        StartCoroutine(SpeedIncrease());
    }

    public void press_Start()
    {
        StartCoroutine(start_running());
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if(speed < maxSpeed)
        {
            speed += 2;
            StartCoroutine(SpeedIncrease());
        }
    }

    public void Pause()
    {
        inGamePanel.SetActive(false);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void playButton()
    {
        inGamePanel.SetActive(true);
        Time.timeScale = time;
        PausePanel.SetActive(false);
    }
}

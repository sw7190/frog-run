using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{

    Rigidbody2D rigi;
    Animator ani;

    public float jumpForce = 7f;

    private bool isJump = false;
    private bool isSlide = false;
    private bool isFall = false;
    private bool isGround = false;
    private int jumpCount = 2;
    private float jumpLimit = 1.8f;

    public float currentTime = 0;

    public Text time;
    public Text speed;

    public GameObject gameOverPanel;



    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        jumpCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                gameOverPanel.SetActive(false);
                transform.position = new Vector3(-4.08f, -2.459086f, 90);
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isSlide= true;
                ani.SetBool("sliding", true);
            }

       

            if ((ani.GetBool("jumping") || ani.GetBool("doubleJumping")) && rigi.velocity.y < 0)
            {
                isFall = true;
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isSlide = false;
                ani.SetBool("sliding", false);
            }
            if (jumpLimit <= transform.position.y)
            {
                rigi.velocity = -Vector3.up;
            }
            currentTime += Time.deltaTime;
            time.text = Mathf.Round(currentTime).ToString() + "s";
            speed.text = "SPEED : " + (Mathf.Floor(currentTime / 5)+1).ToString();

        }
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            isJump = false;
            if (jumpCount > 0) Jump();
        }
        if (isSlide)
        {
            isSlide = false;
            Slide();
        }
        if (isFall)
        {
            isFall = false;
            Fall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Trap") {
            Time.timeScale = 0;
            currentTime = 0;
            gameOverPanel.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !isGround)
        {
            isGround = true;
            if (ani.GetBool("doubleJumping") == true)
            {
                ani.SetBool("doubleJumping", false);
            } else
            {
                ani.SetBool("jumping", false);
            }
            jumpCount = 2;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && isGround)
        {
            isGround = false;
        }
    }

    void Slide()
    {
        rigi.velocity = Vector3.zero;
        rigi.AddForce(-Vector2.up * (jumpForce + 500));
    }
    void Jump()
    {
        if (ani.GetBool("sliding"))
        {
            ani.SetBool("sliding", false);
        }
        if (isGround)
        {
            ani.SetBool("jumping", true);
        }
        else
        {
            ani.SetBool("doubleJumping", true);
            ani.SetBool("jumping", false);
        }

        rigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (jumpLimit <= transform.position.y)
        {
            rigi.velocity = Vector3.zero;
        }
        jumpCount--;
    }
    void Fall()
    {
        rigi.AddForce(-Vector2.up * 10);
    }
}

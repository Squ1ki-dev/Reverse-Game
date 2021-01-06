using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : Unit
{
    private bool isGround;
    private float moveInput;

    [SerializeField] private int lives = 3;
    [SerializeField] private float speedX;
    [SerializeField] private float jumpForce;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float checkRadius;
    
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetPos;

    public int Lives
    {
        get { return lives; }
        set
        {
            if(value < 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    private Animator anim;
    private Rigidbody2D rb;
    
    private void Start()
    {
        speedX = 0f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        livesBar = FindObjectOfType<LivesBar>();
    }

    public void LeftButtonDown()
    {
        speedX = -normalSpeed;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void RightButtonDown()
    {
        if(speedX <= 0f)
        {
            speedX = normalSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void OnButtonUp()
    {
        speedX = 0f;
        anim.SetBool("isRunning", false);
    }

    public void JumpButtonDown()
    {
        if(isGround == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("takeOff");
        }
    }

    private void FixedUpdate()
    {   
        rb.velocity = new Vector2(speedX, rb.velocity.y);
        
        if(speedX != 0f)
            anim.SetBool("isRunning", true);
        
        if(lives < 1)
            SceneManager.LoadScene(3);
    }
    
    private void Update()
    {   
        isGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        if(isGround == true)
            anim.SetBool("isJumping", false);
            
        if(isGround == true)
            anim.SetBool("isJumping", false);
    }
   
    private void OnCollisionEnter2D(Collision2D collision) //Проверка на земле ли игрок
    {
        if(collision.gameObject.tag == "Ground")
            isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision) //Проверка на земле ли игрок
    {
        if(collision.gameObject.tag == "Ground")
            isGround = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other) //Триггер
    {
        if(other.tag == "Trigger")
            SceneManager.LoadScene(3);
            
        if(other.tag == "TheEnd")
            SceneManager.LoadScene(4);
    }

    public override void ReceiveDamage()
    {
        Lives--;
        Debug.Log(lives);
    }
}

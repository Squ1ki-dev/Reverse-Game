using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : Unit
{
    [SerializeField] // Кол-во жизней
    private int lives = 3;
    
    [SerializeField] private float speedX;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkRadius;
    
    [SerializeField] private LayerMask whatIsGround;

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

    private float moveInput;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGround;
    [SerializeField] private LayerMask whatIsGround;
    
    private void Start() //Новый компонент
    {
        speedX = 0f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        livesBar = FindObjectOfType<LivesBar>();
    }

    public void LeftButtonDown() //Левая кнопка нажата
    {
        speedX = -normalSpeed;
        anim.SetBool("isRunning", true);
    }

    public void RightButtonDown() //Правая кнопка нажата
    {
        speedX = normalSpeed;
        anim.SetBool("isRunning", true);
    }

    public void Stop() //СТОП
    {
        speedX = 0f;
        anim.SetBool("isRunning", false);
    }

    public void JumpButtonDown() //Нажатие кнопки прыжка
    {
        if(isGround == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate() //Перемещение
    {   
        transform.Translate(speedX, 0, 0);
        
        if(lives < 1)
        {
            SceneManager.LoadScene(3);
        }
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

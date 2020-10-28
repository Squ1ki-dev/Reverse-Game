using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : Unit
{
    [SerializeField] // Кол-во жизней
    private int lives = 3;

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

    public float speedX;
    public float normalSpeed;
    public float jumpForce;
    public float checkRadius;

    private float moveInput;

    private Rigidbody2D rb; //Физика 2D

    private bool isGround; //Тэг для Триггера
    public LayerMask whatIsGround;
    
    private void Start() //Новый компонент
    {
        livesBar = FindObjectOfType<LivesBar>();
        speedX = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    public void LeftButtonDown() //Левая кнопка нажата
    {
        speedX = -normalSpeed;
    }

    public void RightButtonDown() //Правая кнопка нажата
    {
        speedX = normalSpeed;
    }

    public void Stop() //СТОП
    {
        speedX = 0f;
    }

    public void JumpButtonDown() //Нажатие кнопки прыжка
    {
        if(isGround == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
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
        {
            SceneManager.LoadScene(3);
        }
        if(other.tag == "TheEnd")
        {
            SceneManager.LoadScene(4);
        }
    }

    public override void ReceiveDamage()
    {
        Lives--;
        Debug.Log(lives);
    }
}

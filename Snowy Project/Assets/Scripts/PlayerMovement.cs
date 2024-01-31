using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apricity{

public enum Direction{Left, Right, Up, Down}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, bulletSpeed;
    public Rigidbody2D rb;

    public Sprite frontSprite, leftSprite, rightSprite, backSprite;
    public GameObject bullet, bulletSpawnPointL, bulletSpawnPointR, bulletSpawnPointUp, bulletSpawnPointDown;

    public bool alive = true, invincible = false;
    public Direction direction;

    int damagePerEnemyContact = 1;
    float invTimerDuration = 5, invincibleTimer;
    GameObject bulletSpawnPoint;
    Vector2 moveDirection;
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<Animator>() != null){
            animator = GetComponent<Animator>();
        }else{
            Debug.Log("Main Characrer's animator missing");
        }
        direction = Direction.Down;
        bulletSpawnPoint = bulletSpawnPointDown;
    }

    // Update is called once per frame
    void Update()
    {
        //If player is alive, do the following action
        if(alive){
            ProcessInputs();
        }
        
        //Timer for invincible after getting hit to prevent sudden dead
        if(invincibleTimer > 0 && invincible){
            invincibleTimer -= Time.deltaTime;
        }

        if(invincibleTimer <= 0){
            invincible = false;
        }
    }

    void FixedUpdate()
    {
        Move();
        //Physics Calculation??
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;      //Come back to this

        animator.SetFloat("X_horizontal", moveX);
        animator.SetFloat("Y_vertical", moveY);
        animator.SetFloat("MoveMagnitude", moveDirection.magnitude);

        //change Sprite if go left, right, front, back
        if(moveX > 0){
            bulletSpawnPoint = bulletSpawnPointR;
            direction = Direction.Right;
        }else if(moveX < 0){
            bulletSpawnPoint = bulletSpawnPointL;
            direction = Direction.Left;
        }else if(moveY > 0){
            bulletSpawnPoint = bulletSpawnPointUp;
            direction = Direction.Up;
        }else if(moveY < 0){
            bulletSpawnPoint = bulletSpawnPointDown;
            direction = Direction.Down;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(bullet, bulletSpawnPoint.transform.position, transform.rotation);
            /*if(direction == Direction.Right){
                projectile.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            }else if (direction == Direction.Left){
                projectile.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed);
            }else if (direction == Direction.Up){
                projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
            }else if (direction == Direction.Down){
                projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * bulletSpeed);
            }*/
        }

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && !invincible)
        {
            GameManager.instance.PlayerReceiveDamage(damagePerEnemyContact);           
            invincible = true;
            invincibleTimer = invTimerDuration;
        }
    }
}
}
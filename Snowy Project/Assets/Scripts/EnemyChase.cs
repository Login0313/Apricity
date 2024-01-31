using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apricity{

public class EnemyChase : MonoBehaviour
{

    public GameObject player;
    public float speed;

    private float distance, haltTimer, haltedTime = 5f, freeRagdollTimer, ragdollTime = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //If the halt timer is not 0, stop the enemy movement, otherwise let it chase player or do other action
        if(haltTimer > 0){
            haltTimer -= Time.deltaTime;
        }else{
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if(freeRagdollTimer > 0){
            freeRagdollTimer -= Time.deltaTime;
        }else{
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D col){

        //If the enemy hit the bullet, set halt timer and 
        if (col.gameObject.CompareTag("Bullet"))
        {
            haltTimer = haltedTime;
            freeRagdollTimer = ragdollTime;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
}
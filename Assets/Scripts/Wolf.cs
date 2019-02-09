using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float speed;
    public int health;
    public Transform wallCheck;

    private bool touchedWall = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;   

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        touchedWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (touchedWall)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y); 
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            DamageEnemy();
            //Debug.Log("Sofreu dano");
        }
        
        
    }
    IEnumerator DamageEffect()//retarda a execuçãodo que estta declarado dentor da função
    {
        float actualSpeed = speed;
        speed = speed -1;
        sprite.color = Color.red;
        rb2d.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    void DamageEnemy()
    {
        health--;
        StartCoroutine(DamageEffect());
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}

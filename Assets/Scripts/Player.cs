using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int jumpForce;
    public int healt;
    public Transform groudCheck;
    

    private bool inunarable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Animator anim;
    private Transform trans;

    //variaveis para fazer o ataque
    public float attackRate;
    public Transform spawnAttack;
    public GameObject attackPrefab;
    private float nextAttack = 0f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        //rd2d = GameObject.Find("Wolf").GetComponent<Rigidbody2D>(); // pega de outro personagem
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    
    void Update()
    {
        grounded = Physics2D.Linecast(trans.position,groudCheck.position, 1 << LayerMask.NameToLayer("Ground"));//cria uma linha entre o Player e a layer, visando verificar se o personagem esta tocando-a
        if(Input.GetButtonDown ("Jump") && grounded)
        {
            jumping = true;
        }
        SetAnimations();
        if(Input.GetButton("Fire1") && grounded && Time.time > nextAttack)
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
        if((move <0f && facingRight) || (move >0f && !facingRight)){
            Flip();
        }
        if (jumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
    }
    void SetAnimations()
    {
        anim.SetFloat("VelY", rb2d.velocity.y);
        anim.SetBool("JumpFall", !grounded);
        anim.SetBool("Walk", rb2d.velocity.x != 0f && grounded);
    }

    void Attack()
    {
        anim.SetTrigger("Punch");
        nextAttack = Time.time + attackRate;

        GameObject cloneAttack = Instantiate(attackPrefab, spawnAttack.position, spawnAttack.rotation);
        if (!facingRight)
        {
            cloneAttack.transform.eulerAngles = new Vector3 (180, 0, 180);
        }
    }
}

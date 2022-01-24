using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{  
    public int maxHealth = 100;
    public int currentHealth ;
    public HealthBar healthBar;
     public float movementSpeed = 8;
    public Rigidbody2D _rigidbody;
    public float jumpForce = 6 ;
    public Animator animator;
    public bool IsGrounded;
    private bool IsJumping;
    public LayerMask Ground;
    public float checkRadius ;
    public GameoverMenu GameoverMenu;
    public VictoryScreen VictoryScreen;
    public Transform quay;
    public Vector3 vectorTrai;
    public Vector3 vectorPhai;
    public float hurtForce = -5f;
    public Transform GroundCheck;

        void Start()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(maxHealth);        
        animator =GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        quay = GetComponent<Transform>();
        vectorPhai = new Vector3(0.8f,0.8f,1);
        vectorTrai = new Vector3(-.8f,0.8f,1);
        checkRadius = 0.1f;
    }
 
    void Update()
    {        
        var movement = Input.GetAxis("Horizontal");
        // falling
        if(quay.transform.position.y <= -7.0f){
            Die();
        }

        // face switching
        if(movement < 0){
            quay.transform.localScale = vectorTrai;
           
        }
        else if(movement >0){
            quay.transform.localScale = vectorPhai;
        }
        //run
        animator.SetFloat("Speed",Mathf.Abs(movement));
        transform.position += new Vector3(movement,0,0) *Time.deltaTime * movementSpeed;

        //jump

        // IsGrounded = Physics2D.OverlapCircle(quay.position, checkRadius,Ground);

        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position,checkRadius,Ground);

        if(IsGrounded == true && Input.GetKeyDown(KeyCode.Z)){
            animator.SetBool("IsJumping",true);
            //6
            _rigidbody.velocity = Vector2.up * jumpForce;
            //3000
            // _rigidbody.AddForce(new Vector2(0f,jumpForce));
        }

        if(IsGrounded == true ){
            animator.SetBool("IsJumping",false);
        }

        if(IsGrounded == false ){
            animator.SetBool("IsJumping",true);
        }

        if(IsGrounded == false && Input.GetKeyUp(KeyCode.Z)){
            animator.SetBool("IsJumping",false);
        }

    }
    // HURT
    private void OnCollisionEnter2D(Collision2D other) {
        // dynamic enemy
        if(other.gameObject.tag == "Enemy"){
            currentHealth = currentHealth - 20;
            healthBar.setHealth(currentHealth);
            if(currentHealth <= 0 ){
               Die();
            }
            else{
                animator.SetTrigger("Being hit");
                if(other.gameObject.transform.position.x > quay.position.x){
                    _rigidbody.velocity = new Vector2(hurtForce,_rigidbody.velocity.y);
                }
                else{
                    _rigidbody.velocity = new Vector2(-hurtForce,_rigidbody.velocity.y);
                }
                // StartCoroutine ("GetInvulnerable");
            }
        }
        //static enemy
        if(other.gameObject.tag == "Static Enemy"){
            currentHealth = currentHealth - 50;
            healthBar.setHealth(currentHealth);
            if(currentHealth <= 0 ){
               Die();
            }
            else{
                animator.SetTrigger("Being hit");
                if(other.gameObject.transform.position.x > quay.position.x){
                    _rigidbody.velocity = new Vector2(hurtForce,_rigidbody.velocity.y);
                }
                else{
                    _rigidbody.velocity = new Vector2(-hurtForce,_rigidbody.velocity.y);
                }
            }    
        }
    }

    //DIE
    private void Die(){
        animator.SetTrigger("Die");
        Destroy(gameObject,1f);
        GameoverMenu.Setup();
        // SceneManager.LoadScene("SampleScene");
    }
    // regen
    private void OnTriggerEnter2D(Collider2D other) {
        // potions
        if(other.gameObject.tag == "Potions"){
            currentHealth += 30;
            healthBar.setHealth(currentHealth);
            Destroy(other.gameObject);
        }
        // CHEST
        if(other.gameObject.tag == "Chest"){
            Debug.Log("victory");
            VictoryScreen.Setup();
        }        
    }


}    


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDies : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public Animator animator;
    public HealthBar healthBar;
    int currentHealth ;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(maxHealth);
    }
    public void TakeDamage(int damage){
        gameObject.GetComponent<EnemyPatrol>().startDaze();
        currentHealth -= damage;
        animator.SetTrigger("BeingHit");
        healthBar.setHealth(currentHealth);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy's Dead");
        //animator.SetBool("isDead", true);
        // this.enabled = false;
        // GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,0.4f);
    }
}

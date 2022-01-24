using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int AttackDamage = 40;
    public Animator animator;
    public Transform attackpoint;
    public float attackRange = 0.8f;
    public LayerMask enemyLayers;
    public float attackPerSec = 1;
    float attackTime = 0;
    void Update()
    {
        if(Time.time > attackTime)
        if(Input.GetKeyDown(KeyCode.C)){
            Attack();
            attackTime = Time.time + 1/ attackPerSec;          
        }
    }
    void Attack (){
        animator.SetTrigger("Attack");
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position,attackRange,enemyLayers);
        foreach(Collider2D enemies in hitEnemies){
            enemies.GetComponent<EnemyDies>().TakeDamage(AttackDamage);
        } 
    }

     void OnDrawGizmosSelected() {
        if(attackpoint == null)
        return;
        Gizmos.DrawWireSphere(attackpoint.position,attackRange);
    }
}


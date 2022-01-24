using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    //distance = 2
    public float distance;
    public bool movingLeft = true ;
    public Animator animator;
    public Transform groundDetection;
    public Collider2D bodyCollider;
    public LayerMask GroundLayer;
    public float DazedTime;
    public float StartDazedTime;
    void Update()
    {
        if(DazedTime <= 0){
        speed = 2;    
        transform.Translate(Vector2.left * speed *Time.deltaTime);
        animator.SetFloat("Speed",speed);
        }
        else{
            speed = 0;
            DazedTime -= Time.deltaTime;
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position ,Vector2.down,distance);
        if(groundInfo.collider == false || bodyCollider.IsTouchingLayers(GroundLayer)){
            if(movingLeft == true){
                transform.eulerAngles = new Vector3(0,-180,0);
                movingLeft = false;

            }
            else {
                transform.eulerAngles = new Vector3 (0,0,0);
                movingLeft = true;
            }
        }
    }
    public void startDaze(){
        DazedTime = StartDazedTime;
    }
}

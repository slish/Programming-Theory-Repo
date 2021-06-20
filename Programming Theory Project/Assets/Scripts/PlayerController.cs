using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody playerRb;
    public float jumpForce;
    public float rightCorrection = 1.2f;
    public bool isGrounded = true;
    public bool canDoubleJump = true;
    public int jumpTimes = 2;
    private TowerBehavior towerBehavior;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
        rb.inertiaTensorRotation = Quaternion.identity;
        towerBehavior = GameObject.Find("Tower").GetComponent<TowerBehavior>();
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DeathZone")){
            towerBehavior.setGameOver();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other){
        //if (other.CompareTag("Ground")){
            isGrounded = true;
            canDoubleJump = true;
            jumpTimes = 2;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal")<0){
            rightCorrection = 1f;
        }else{
            rightCorrection = 1.2f * towerBehavior.getSpeed()/20;
        }
        float movement = Input.GetAxis("Horizontal") * speed * rightCorrection * Time.deltaTime * -1;
        transform.Rotate(0, movement, 0);
        
        if (Input.GetKeyDown("space")){
            
            if (isGrounded){
                isGrounded = false;
                Jump();
            } else {
                if (canDoubleJump){
                    canDoubleJump = false;
                    Vector3 velocity = playerRb.velocity;
                    velocity.y = 0;
                    playerRb.velocity = velocity;
                    Jump();
                }
            }
            jumpTimes --;
            
        }
    }

    void Jump(){
        playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}

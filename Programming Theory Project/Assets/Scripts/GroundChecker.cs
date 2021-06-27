using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool grounded;
    private bool canDoubleJump;
    private PlayerController playerCt;

    void Awake(){
        playerCt = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Ground")){
            grounded = true;
            canDoubleJump = true;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(playerCt);
        Debug.Log(other.gameObject.GetComponent<CoinController>());
        if (other.CompareTag("DeathZone")){
            playerCt.TriggerDeath();
        }
        if (other.CompareTag("Coin")){
            playerCt.PickUpCoin(other.gameObject.GetComponent<CoinController>(),
                                this.transform.position);
        }
    }

    public bool IsGrounded(){
        return grounded;
    }

    public void setGrounded(bool groundChange){
        grounded = groundChange;
    }

    public bool DoubleJump(){
        return canDoubleJump;
    }

     public void SetDoubleJump(bool doubleChange){
        canDoubleJump = doubleChange;
    }
}

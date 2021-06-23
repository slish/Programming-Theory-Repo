using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TMP_Text playerScoreText;
    public TMP_Text scorePopUp;
    public ParticleSystem coinParticles;

    // Start is called before the first frame update
    void Start()
    {   
        GameManager.Instance.ResetScore();
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
        rb.inertiaTensorRotation = Quaternion.identity;
        towerBehavior = GameObject.Find("Tower").GetComponent<TowerBehavior>();
        StartCoroutine(constantScore());
    }

    private IEnumerator constantScore(){
        while (!towerBehavior.getGameOverStatus()){
            GameManager.Instance.AddScore(towerBehavior.getSpeed());
            playerScoreText.SetText("Score : " + GameManager.Instance.GetScore());
            yield return new WaitForSeconds(2);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DeathZone")){
            towerBehavior.setGameOver();
            Destroy(gameObject);
        }
        if (other.CompareTag("Coin")){
            CoinController coin = other.gameObject.GetComponent<CoinController>();
            float score = coin.GetValue();
            scorePopUp.SetText(score.ToString());
            Instantiate(coinParticles, other.transform.position, coinParticles.transform.rotation);
            Instantiate(scorePopUp, other.transform.position, scorePopUp.transform.rotation);
            GameManager.Instance.AddScore(score);
            playerScoreText.SetText("Score : " + GameManager.Instance.GetScore());
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            canDoubleJump = true;
            jumpTimes = 2;
        }
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

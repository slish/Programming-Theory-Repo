using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float zSpeed;
    //public Rigidbody playerRb;
    public GameObject playerModel;
    public GameObject camera;
    private Rigidbody playerModelRb;
    private GroundChecker groundCheck;
    public float jumpForce;
    public float rightCorrection;
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
        playerModelRb = playerModel.GetComponent<Rigidbody>();
        groundCheck = playerModel.GetComponent<GroundChecker>();
        playerModelRb.centerOfMass = Vector3.zero;
        playerModelRb.inertiaTensorRotation = Quaternion.identity;
        //Rigidbody rb = this.GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0,0,0);
        //rb.inertiaTensorRotation = Quaternion.identity;
        towerBehavior = GameObject.Find("Rotator").GetComponent<TowerBehavior>();
        StartCoroutine(constantScore());
    }

    private IEnumerator constantScore(){
        while (!towerBehavior.getGameOverStatus()){
            GameManager.Instance.AddScore(towerBehavior.getSpeed());
            playerScoreText.SetText("Score : " + GameManager.Instance.GetScore());
            yield return new WaitForSeconds(2);
        }
    }

    public void TriggerDeath(){
        towerBehavior.setGameOver();
        Destroy(gameObject);
    }

    public void PickUpCoin(CoinController coin, Vector3 coinPosition){
        //CoinController coin = other.gameObject.GetComponent<CoinController>();
        float score = coin.GetValue();
        scorePopUp.SetText(score.ToString());
        Instantiate(coinParticles, coinPosition, coinParticles.transform.rotation);
        Instantiate(scorePopUp, coinPosition, scorePopUp.transform.rotation);
        GameManager.Instance.AddScore(score);
        playerScoreText.SetText("Score : " + GameManager.Instance.GetScore());
        Destroy(coin.gameObject);
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DeathZone")){
            
        }
        if (other.CompareTag("Coin")){
            
        }
    }
    /*void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            canDoubleJump = true;
            jumpTimes = 2;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if(groundCheck.IsGrounded()){
            isGrounded = true;
        }else{
            isGrounded = false;
        }
        // Rotation of the player
        float movement = 0;
        //Debug.Log(transform.eulerAngles.y + towerBehavior.transform.eulerAngles.y);
        //Debug.Log(camera.transform.eulerAngles.y);
        if(camera.transform.eulerAngles.y > 332
           || camera.transform.eulerAngles.y < 28){
                if (Input.GetAxis("Horizontal")<0){
                    movement = 
                    Input.GetAxis("Horizontal") * speed * Time.deltaTime * -1;
                }else {
                    rightCorrection = towerBehavior.getSpeed() * Time.deltaTime;
                    movement = 
                        Input.GetAxis("Horizontal") * speed * Time.deltaTime * -1 - 
                        (rightCorrection * Input.GetAxis("Horizontal"));
                }
                
        }else if(camera.transform.eulerAngles.y > 332){
            rightCorrection = towerBehavior.getSpeed() * Time.deltaTime;
                movement = 
                    Input.GetAxis("Horizontal") * speed * Time.deltaTime * -1 - 
                    (rightCorrection * Input.GetAxis("Horizontal"));
        }
        if(camera.transform.eulerAngles.y > 28 && camera.transform.eulerAngles.y < 29){
            transform.Rotate(0,-towerBehavior.getSpeed()*Time.deltaTime*1.05f,0);
        }
        transform.Rotate(0, movement, 0);

        // Back and forth movement
        /*float zMovement = zSpeed * Input.GetAxis("Vertical")*Time.deltaTime;
        Debug.Log(zMovement);
        playerModelRb.AddRelativeForce(0, 0, zMovement, ForceMode.Impulse);  */      
        
        if (Input.GetKeyDown("space")){
            
            if (groundCheck.IsGrounded()){
                groundCheck.setGrounded(false);
                //isGrounded = false;
                Jump();
            } else {
                if (groundCheck.DoubleJump()){
                    groundCheck.SetDoubleJump(false);
                    //canDoubleJump = false;
                    Vector3 velocity = playerModelRb.velocity;
                    velocity.y = 0;
                    playerModelRb.velocity = velocity;
                    Jump();
                }
            }
            jumpTimes --;
            
        }
    }

    void LateUpdate(){
        /*var lookPos = Vector3.zero - playerModel.transform.position;
        lookPos.y = 0;
        var playerRotation = Quaternion.LookRotation(lookPos);
        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, playerRotation, Time.deltaTime);
        playerModel.transform.LookAt(Vector3.zero);*/
    }

    void Jump(){
        playerModelRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}

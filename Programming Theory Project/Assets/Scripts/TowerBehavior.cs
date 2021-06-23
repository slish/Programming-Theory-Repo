using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBehavior : MonoBehaviour
{
    public float rotationSpeed;
    public float materialScrollSpeed;
    public GameObject[] steps;
    public GameObject[] traps;
    public GameObject[] pickups;
    public GameObject player;
    public GameObject highScoreText;
    private Renderer towerRenderer;
    public float stepSpawnTime;
    public float trapSpawnTime;
    public float coinSpawnTime;
    public float difficultyIncreaseTimer;
    private bool isGameOver = false;
    private bool firstTrap = true;
    public Canvas gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        towerRenderer = GetComponent<Renderer>();
        StartCoroutine(spawnSteps());
        StartCoroutine(spawnTraps());
        StartCoroutine(spawnCoins());
        StartCoroutine(speedUpTower());
    }

    private IEnumerator spawnSteps(){
        while (!isGameOver){
            
            int stepIndex = Random.Range(0, steps.Length);
            Vector3 randomPosition = new Vector3(0, Random.Range(2,4), 5);
            GameObject childObject = Instantiate(steps[stepIndex],
                                                 randomPosition,
                                                 steps[stepIndex].transform.rotation);
            childObject.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(stepSpawnTime);
        }
    }

    private IEnumerator spawnTraps(){
        while (!isGameOver){ 
            if (!firstTrap){
                int stepIndex = Random.Range(0, traps.Length);
                Vector3 randomPosition = new Vector3(0, Random.Range(2,4), 5);
                GameObject childObject = Instantiate(traps[stepIndex],
                                                    randomPosition,
                                                    traps[stepIndex].transform.rotation);
                childObject.transform.parent = gameObject.transform;
            }
            firstTrap = false;
            yield return new WaitForSeconds(trapSpawnTime);
        }
    }

    private IEnumerator spawnCoins(){
        while (!isGameOver){
            int pickUpIndex = Random.Range(0, pickups.Length);
            Vector3 randomPosition = new Vector3(0, Random.Range(4.5f,5.5f), 5.5f);
            GameObject childObject = Instantiate(pickups[pickUpIndex],
                                                    randomPosition,
                                                    pickups[pickUpIndex].transform.rotation);
                childObject.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(Random.Range(coinSpawnTime, coinSpawnTime+1));
        }
    }

    private IEnumerator speedUpTower(){
        while (!isGameOver){
            rotationSpeed += 5;
            stepSpawnTime *= .95f;
            CoinController.SetValue(rotationSpeed*3);
            yield return new WaitForSeconds(difficultyIncreaseTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver){
            gameObject.transform.Rotate( Vector3.up * (rotationSpeed * Time.deltaTime));
            player.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
            float offset = materialScrollSpeed * Time.deltaTime * (rotationSpeed/10);
            
            towerRenderer.material.mainTextureOffset += new Vector2(0,0.1f) * rotationSpeed/10 * Time.deltaTime;
        }
    }

    public float getSpeed(){
        return rotationSpeed;
    }

    // ABSTRACTION
    public void setGameOver(){
        GameManager.Instance.calculateHighScores();
        GameManager.Instance.setText(highScoreText);
        
        Instantiate(gameOverUI, new Vector3(0,0,0), gameOverUI.transform.rotation);
        isGameOver = true;
        
    }

    public bool getGameOverStatus(){
        return isGameOver;
    }
}

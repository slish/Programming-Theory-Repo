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
    public GameObject tower;
    public GameObject highScoreText;
    private GameObject[] towerArray = new GameObject[4];
    private int towerIndex;
    private GameObject lastTower;
    private Renderer towerRenderer;
    private Rigidbody towerRb;
    private float stepSpawnTime;
    public float DIFFICULTYINCREASESTEPS;
    private float difficultyIncreaseCounter;
    private bool isGameOver = false;
    private bool canSpawnAnotherTower;
    public Canvas gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        stepSpawnTime = 180 / rotationSpeed;
        towerArray[0] = Instantiate(tower, new Vector3(0, -5, 0), gameObject.transform.rotation);
        towerArray[0].transform.parent = gameObject.transform;
        towerArray[1] = Instantiate(tower, new Vector3(0, 5, 0), gameObject.transform.rotation);
        towerArray[1].transform.parent = gameObject.transform;
        towerArray[2] = Instantiate(tower, new Vector3(0, 15, 0), gameObject.transform.rotation);
        towerArray[2].transform.parent = gameObject.transform;
        lastTower = towerArray[2];
        towerIndex = 3;
        GameObject[] bgTower1 = TowerSpawner(5, 10, -5, 10);
        GameObject[] bgTower2 = TowerSpawner(10, 20, -30, -30);
        canSpawnAnotherTower = false;
        towerRenderer = GetComponent<Renderer>();
        towerRb = GetComponent<Rigidbody>();
        difficultyIncreaseCounter = DIFFICULTYINCREASESTEPS;
        StartCoroutine(spawnSteps());
    }

    private IEnumerator spawnSteps(){
        while (!isGameOver){
            
            int stepIndex = Random.Range(0, steps.Length);
            Vector3 randomPosition = new Vector3(0, Random.Range(6,7), 0);
            GameObject childObject = Instantiate(steps[stepIndex],
                                                 randomPosition,
                                                 steps[stepIndex].transform.rotation);
            childObject.transform.parent = gameObject.transform;
            difficultyIncreaseCounter --;
            Debug.Log(stepSpawnTime);
            if(difficultyIncreaseCounter == 0){
                difficultyIncreaseCounter = DIFFICULTYINCREASESTEPS;
                rotationSpeed += 5;
                stepSpawnTime = 180 / rotationSpeed;
                
            }
            yield return new WaitForSeconds(stepSpawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver){
            gameObject.transform.Rotate( Vector3.up * (rotationSpeed * Time.deltaTime));
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * (rotationSpeed/30));
            if(lastTower.transform.position.y < 10){
                if(towerArray[towerIndex] != null){
                    Destroy(towerArray[towerIndex]);
                }
                towerArray[towerIndex] = 
                    Instantiate(tower, 
                                new Vector3(0, lastTower.transform.position.y + 10, 0), 
                                gameObject.transform.rotation);
                towerArray[towerIndex].transform.parent = gameObject.transform;
                lastTower = towerArray[towerIndex];
                if(towerIndex == 3){
                    towerIndex = 0;
                } else{
                    towerIndex ++;
                }
            }
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

    public GameObject[] TowerSpawner(int numSegments, float xPos, float yPos, float zPos){
        GameObject[] createdTower = new GameObject[numSegments];
        for(int i = 0; i < numSegments; i++){
            createdTower[i] = Instantiate(tower, new Vector3(xPos, yPos + i*10, zPos), gameObject.transform.rotation);
            createdTower[i].transform.parent = gameObject.transform;
        }
        return createdTower;
    }
}

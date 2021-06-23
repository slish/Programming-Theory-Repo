using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public float playerScore;
    public float[] highScores = new float[10];
    public string[] highUsers = new string[10];
    void Awake()
    {
        Debug.Log("Playerscore in Awake() is " + playerScore);
        if (Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        LoadPrefs();
    }

    public void setPlayerName(string playerName){
        this.playerName = playerName;
    }

    public string getPlayerName(){
        return playerName;
    }

    public void changeLevel(int numLevel){
        SceneManager.LoadScene(numLevel);
    }

    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnApplicationQuit(){
        SavePrefs();
    }

    public void AddScore(float value){
        playerScore += value;
    }
    public float GetScore(){
        return playerScore;
    }
    public void ResetScore(){
        playerScore = 0;
    }

    public void SavePrefs(){
        for(int i = 0; i<highScores.Length; i++){
            PlayerPrefs.SetFloat("Score"+i, highScores[i]);
            PlayerPrefs.SetString("User"+i, highUsers[i]);
        }
        PlayerPrefs.Save();
    }

    public void LoadPrefs(){
        for(int i = 0; i<highScores.Length; i++){
            highScores[i] = PlayerPrefs.GetFloat("Score"+i, 0);
            highUsers[i] = PlayerPrefs.GetString("User"+i);
        }
    }

    public void calculateHighScores(){
        float newValue;
        string newName;
        float lowerValue;
        string lowerName;
        for(int i = 0; i < highScores.Length; i++){
            if(playerScore > highScores[i]){
                newValue = highScores[i];
                newName = highUsers[i];
                for(int j = i; j < highScores.Length; j++){
                    if(j+1 < highScores.Length){
                        lowerValue = highScores[j+1];
                        highScores[j+1] = newValue;
                        newValue = lowerValue;

                        lowerName = highUsers[j+1];
                        highUsers[j+1] = newName;
                        newName = lowerName;
                    }
                }
                highScores[i] = playerScore;
                highUsers[i] = playerName;
                break;
            }
        }
    }

    public void setText(GameObject textObject){
        string highScoreText = "HIGH SCORE: \n";
        TMP_Text actualText = textObject.GetComponentInChildren<TMP_Text>();
        Debug.Log(actualText);
        for(int i = 0; i < highScores.Length; i++){
            int j = i + 1;
            highScoreText = highScoreText + j + ". " + highUsers[i] + " : " + highScores[i] + "\n";
        }
        actualText.SetText(highScoreText);
    }
}

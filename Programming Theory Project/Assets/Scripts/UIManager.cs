using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        playerNameInputField.Select();
        playerNameInputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePlayerName() {
        GameManager.Instance.setPlayerName(playerNameInputField.text);
        if (!string.IsNullOrEmpty(GameManager.Instance.getPlayerName())){
            startButton.interactable = true;
        } else{
            startButton.interactable = false;
        }
    }
}

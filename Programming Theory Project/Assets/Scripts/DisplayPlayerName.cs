using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
    public TMP_Text playerText;
    // Start is called before the first frame update
    void Start(){
        playerText = gameObject.GetComponent<TMP_Text>();
        playerText.SetText("Player : " + GameManager.Instance.getPlayerName());
    }
}

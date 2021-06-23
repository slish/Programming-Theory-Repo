using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScorePopupController : MonoBehaviour
{
    public float yMoveSpeed;
    public float disappearTimer;
    private float MAXDISAPPEARTIME;
    private Color textColor;
    private TMP_Text text;

    private void Awake(){
        text = gameObject.GetComponent<TMP_Text>();
        textColor = text.color;
        MAXDISAPPEARTIME = disappearTimer;
    }
    void Update()
    {
        transform.position += new Vector3(0, yMoveSpeed, 0) * Time.deltaTime;

        if(disappearTimer > MAXDISAPPEARTIME/2){
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else{
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0){
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }
}

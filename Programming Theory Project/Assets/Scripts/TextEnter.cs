using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleUpText());
    }

    IEnumerator ScaleUpText(){
        float timePassed = 0;
        while (timePassed < 3){
            gameObject.transform.localScale += new Vector3(0.1f, .1f, .1f) * 1 * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

}

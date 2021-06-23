using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed;    
    public TowerBehavior towerBehavior;
    public static float coinValue;
    // Start is called before the first frame update
    void Start()
    {
        towerBehavior = GameObject.Find("Tower").GetComponent<TowerBehavior>();
        coinValue = (towerBehavior.getSpeed()*3);
    }

    // ENCAPSULATION
    protected virtual void Update()
    {
        if (!towerBehavior.getGameOverStatus()){
            gameObject.transform.Translate(
                Vector3.down * speed * (towerBehavior.getSpeed()/10) * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DeathZone") || other.CompareTag("Trap")){
            Destroy(gameObject);
        }
    }

    public static void SetValue(float value){
        coinValue = value;
    }

    public float GetValue(){
        return coinValue;
    }
}

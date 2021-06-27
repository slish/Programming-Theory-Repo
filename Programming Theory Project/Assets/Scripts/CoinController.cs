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
        towerBehavior = GameObject.Find("Rotator").GetComponent<TowerBehavior>();
        coinValue = (towerBehavior.getSpeed()*3);
    }

    // ENCAPSULATION
    protected virtual void Update()
    {
        
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

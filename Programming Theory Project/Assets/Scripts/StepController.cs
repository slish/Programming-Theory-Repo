using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    public float speed;
    public TowerBehavior towerBehavior;
    // Start is called before the first frame update
    void Start()
    {
        towerBehavior = GameObject.Find("Tower").GetComponent<TowerBehavior>();
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
        if (other.CompareTag("DeathZone")){
            Destroy(gameObject);
        }
    }
}

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

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * speed * (towerBehavior.getSpeed()/10) * Time.deltaTime);
    }
}

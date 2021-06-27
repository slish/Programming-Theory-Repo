using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class LiftStepController : StepController
{
    private bool goingDown;
    public float liftTimer = 1f;
    public float rotationSpeed;

    void Start()
    {
        towerBehavior = GameObject.Find("Rotator").GetComponent<TowerBehavior>();
        StartCoroutine(liftRoutine());
    }

    // POLYMORPHISM
    protected override void Update()
    {
        if (goingDown){
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        } else{
            transform.Rotate(Vector3.back * -rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator liftRoutine(){
        while (!towerBehavior.getGameOverStatus()){
            goingDown = !goingDown;
            yield return new WaitForSeconds(liftTimer);
        }
    }

}

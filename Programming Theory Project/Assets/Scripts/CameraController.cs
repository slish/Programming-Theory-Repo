using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (player != null){
            float yRotationValue = player.transform.rotation.y;
            float yPositionValue = player.transform.position.y;
            Quaternion camRotation = Quaternion.Euler(0, yRotationValue*100, 0);
            Vector3 camPosition = new Vector3(0, yPositionValue/2, 0);
            transform.rotation = camRotation;
            transform.position = camPosition;
        }
        
    }
}

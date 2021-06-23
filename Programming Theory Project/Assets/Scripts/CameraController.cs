using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camHorizontalRotationFollowAmount;
    public float camVerticalRotationFollowAmount;
    public float camYPositionFollowAmount;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (player != null){
            // Change position and rotation of camera based on values set in the Inspector
            float yRotationValue = player.transform.rotation.y;
            float xRotationValue = player.transform.rotation.x;
            float yPositionValue = player.transform.position.y;
            Quaternion camRotation = 
                Quaternion.Euler(
                    yPositionValue * camVerticalRotationFollowAmount, 
                    yRotationValue * camHorizontalRotationFollowAmount, 0);
            Vector3 camPosition = new Vector3(0, yPositionValue * camYPositionFollowAmount, 0);
            transform.rotation = camRotation;
            transform.position = camPosition;
        }
        
    }
}

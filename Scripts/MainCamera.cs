using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{   

    private float angle;
    private float maxVAngle;
    private float minVAngle;
    private float camDist;
    private float _yaw; 

    // Start is called before the first frame update
    private void Start(){
        angle = 60.0f;
        maxVAngle = 50.0f;
        minVAngle = 5.0f;
        camDist = 125.0f;
        _yaw = 10.0f;
        transform.eulerAngles = new Vector3(0.0f, angle, 0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput(){

        float deltaAngle = 0.0f;

        if(Input.GetKey(KeyCode.LeftArrow)){
            deltaAngle = 1.5f;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            deltaAngle = -1.5f;
        }
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)){
            deltaAngle = 0;
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            _yaw += 0.5f;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            _yaw -= 0.5f;
        }
        _yaw = Mathf.Max(minVAngle, Mathf.Min(maxVAngle, _yaw));
        camDist -= Input.mouseScrollDelta.y;



        Vector3 oldAngles = transform.eulerAngles;
        angle = (oldAngles.y + deltaAngle)%360.0f;
        transform.eulerAngles = new Vector3(_yaw, angle, oldAngles.z);
        ChangeRotPosition();

    }

    private void ChangeRotPosition(){

        float yPos = Mathf.Sin((_yaw*Mathf.PI)/180.0f) * camDist;
        float xzShrink = Mathf.Cos((_yaw*Mathf.PI)/180.0f);

        float zPos = -(Mathf.Cos((angle*Mathf.PI)/180.0f) * camDist * xzShrink);
        float xPos = -(Mathf.Sin((angle*Mathf.PI)/180.0f) * camDist * xzShrink);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}

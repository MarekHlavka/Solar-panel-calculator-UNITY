using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    // Panel angle = 21

    public GameObject light;

    private float sunDst;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.Find("Light");
        sunDst = 20.0f;
        angle = 90.0f;
        setPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)){
            angle+=0.0f;
        }
        else{
            if(Input.GetKey(KeyCode.A)){
                angle-=.5f;
            }
            if(Input.GetKey(KeyCode.D)){
                angle+=.5f;
            }
        }
        angle = GameHandler.GetSAS();
        angle = Mathf.Max(.0f, Mathf.Min(angle, 180.0f));
        setPos();
        
    }

    private void setPos(){
        float yPos = Mathf.Sin((angle*Mathf.PI)/180.0f)*sunDst;
        float xPos = -Mathf.Cos((angle*Mathf.PI)/180.0f)*sunDst;
        //transform.position = new Vector3(xPos, yPos, 0.0f);
    }
}

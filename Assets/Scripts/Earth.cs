using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class Earth : MonoBehaviour
{

    [SerializeField] private GameObject _earth;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _flagPlane;

    private float earth_lat; // y rotation
    private float earth_long; // z rotation
    [SerializeField] private float xFlagOffset;
    [SerializeField] private float _flagAngle;
    private float flag_y;
    private const float flagDistance = 1.25f;
    private Vector3 originalPos;
    private Vector3 origEuler;

    private Vector2 _position;


    // Start is called before the first frame update
    void Start()
    {
        
        earth_lat = 0.0f;
        earth_long = 90.0f;
        xFlagOffset = 40.0f;
        _flagAngle = 0f;
        flag_y = 0.0f;
        originalPos = _flag.transform.position;
        
        _position = new Vector2(earth_long - 45, flag_y);
        origEuler = _flagPlane.transform.eulerAngles;
        _earth.transform.position = originalPos;

        rotate();
        

    }

    // Update is called once per frame
    void Update()
    {
        _position = GameHandler.GetPosition();//new Vector2(earth_long - 135, flag_y);
        rotate();
        //GameHandler.SetPosition(position);
    }

    private void rotate(){
        _earth.transform.position = originalPos;
        _earth.transform.eulerAngles = new Vector3(0, _position.y + 90f + xFlagOffset, 0);
        Debug.Log(_position);
        
        float yDiff = Mathf.Sin((_position.x + 0) * 1.1f * Mathf.Deg2Rad) * flagDistance;
        float cosY = Mathf.Cos((_position.x + 0) * 1.1f * Mathf.Deg2Rad);
        float xDiff = -Mathf.Sin(xFlagOffset * Mathf.Deg2Rad) * flagDistance * cosY;
        float zDiff = -Mathf.Cos(xFlagOffset * Mathf.Deg2Rad) * flagDistance * cosY;
        

        _flag.transform.position = new Vector3(originalPos.x + xDiff , originalPos.y + yDiff, originalPos.z + zDiff);
        float x_rotation = (_position.x * -1f) + 90f;
        float z_rotation = (Mathf.Abs(_position.x-90f))/9f;
        float y_rotation = Mathf.Abs(_position.x-90f)/4f;
        _flag.transform.eulerAngles = new Vector3(x_rotation, 180f, -z_rotation);
        origEuler = _flag.transform.eulerAngles;
        

        /// DODELAT
        // 90 -> 0
        // -90 -> 180
        // 0 ->90
    }
}

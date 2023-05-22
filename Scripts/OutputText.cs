using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputText : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    private float angle;
    private float clouds;
    private float temp;
    private float fresnel;
    private float output;
    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        angle = clouds = temp = fresnel = 0.0f;
    }

    

    // Update is called once per frame
    void Update()
    {
        angle = GameHandler.GetSAS();
        clouds = GameHandler.GetCS();
        temp = GameHandler.GetTS();
        fresnel = GameHandler.GetFS();
        position = GameHandler.GetPosition();
        output = GameHandler.GetOutput();

        calcOutput();
    }

    private void calcOutput(){
       /* _text.text = angle.ToString() + '\n'
            + clouds.ToString() + '\n'
            + position.x.ToString() +'\n'
            + position.y.ToString() + '\n'
            + output.ToString();*/
        _text.text = output.ToString("0.000") + "(kWh/year)";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            Debug.Log(v.ToString("0.000"));
            GameHandler.SetTS(v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        float temp = GameHandler.GetTS();
        _text.text = "Temp - " + (temp+25f).ToString("0.00") + "°C";
    }
}

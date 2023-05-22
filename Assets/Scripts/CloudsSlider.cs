using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudsSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            Debug.Log(v.ToString("0.000"));
            GameHandler.SetCS(v);
        });
    }

    void Update(){
        float clouds = GameHandler.GetCS();
        _text.text = "Clouds coverage - " + (clouds*100).ToString("0.00") + "%";
    }
}

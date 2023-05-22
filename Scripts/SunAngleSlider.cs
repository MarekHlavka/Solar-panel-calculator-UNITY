using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunAngleSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text _text;
    private float _size;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            Debug.Log(v.ToString("0.000"));
            GameHandler.SetSAS(v);
        });
        _size = 4f;
        panel.transform.localScale = new Vector3(4f, 4f, 4f);
    }

    void Update(){
        _size = GameHandler.GetSAS();
        panel.transform.localScale = new Vector3(Mathf.Sqrt(_size/4f)*4f, 4f, Mathf.Sqrt(_size/4f)*4f);
        _text.text = "Panel Size " + (_size/4f).ToString("0.00") + " m2";
    }

}

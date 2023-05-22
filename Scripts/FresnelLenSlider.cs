using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FresnelLenSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            Debug.Log(v.ToString("0.000"));
            GameHandler.SetFS(v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = GameHandler.GetFS();
        _text.text = "Lens size ratio " + ratio.ToString("0") + ":1";
    }
}

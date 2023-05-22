using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityDropDown : MonoBehaviour
{

    [SerializeField] private Dropdown _dropdown;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown.onValueChanged.AddListener(delegate{ChangeVal(_dropdown);});
        List<string> cityNames = new List<string> {
            "Brno",
            "London",
            "New York",
            "Tokyo",
            "Oslo",
            "Mexico City",
            "Mumbai",
            "Nairobi",
            "Rio de Janeiro",
            "Beijing",
            "Sydney",
            "Casablanca",
            "Vancouver",
            "Cape Town",
            "Helsinki"
        };

        _dropdown.AddOptions(cityNames);
    }

    void ChangeVal(Dropdown dropdown){
        Debug.Log(dropdown.value.ToString("0.000"));
        GameHandler.SetCDD(dropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

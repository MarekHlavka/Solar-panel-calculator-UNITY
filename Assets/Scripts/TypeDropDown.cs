using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TypeDropDown : MonoBehaviour
{

    [SerializeField] private Dropdown _dropdown;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown.onValueChanged.AddListener(delegate{ChangeVal(_dropdown);});
    }
    void ChangeVal(Dropdown dropdown){
        Debug.Log(dropdown.value.ToString("0.000"));
        GameHandler.SetTDD(dropdown.value);
        _GetText();
        
    }

    private IEnumerator _GetText(){
        Debug.Log("prepre");
        UnityWebRequest www = UnityWebRequest.Get("https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=2023-03-12");
        Debug.Log("pre");
        yield return www.SendWebRequest();
        Debug.Log("post");
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

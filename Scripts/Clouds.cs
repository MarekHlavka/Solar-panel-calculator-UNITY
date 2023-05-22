using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    [SerializeField] private GameObject _cloud;
    [SerializeField] private ParticleSystem photons;

    private float clouds;
    private float photonsOrigCnt;
    private const float cloudsHeight = 100f; 
    private const float cloudsRadius = 150f;
    private GameObject[] cloudsObjects = new GameObject[100];
    private const int cnt = 100;

    // Start is called before the first frame update
    void Start()
    {   
        photonsOrigCnt = photons.emissionRate;
        clouds = 0f;
        for (int i = 0; i < cnt; i++){
            float x = Random.Range(-cloudsRadius, cloudsRadius);
            float z = Random.Range(-cloudsRadius, cloudsRadius);
            cloudsObjects[i] = (GameObject)Instantiate(_cloud, new Vector3(x, cloudsHeight, z), Quaternion.identity);
            cloudsObjects[i].transform.eulerAngles = new Vector3(0,Random.Range(0,360),0);
            float scale = Random.Range(0.5f, 2f);
            cloudsObjects[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        clouds = GameHandler.GetCS();
        float photonsCnt = 1f - clouds*0.8f;
        photons.emissionRate = photonsOrigCnt * photonsCnt;


        // Instancovat mraky náhodně na plochu
        // SetActive False / True
        // Prosty push a pop

        for (int i = 0; i < cnt; i++){
            if(i<cnt * clouds){
                cloudsObjects[i].SetActive(true);
            }
            else{
                cloudsObjects[i].SetActive(false);
            }
            
        }



        

    }
}

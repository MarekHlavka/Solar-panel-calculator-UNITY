using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Material mat;

    public static GameHandler instance;

    private static float sunAngleSlider; // Area
    private static float azimuthSlider;
    private static float cloudSlider;
    private static float tempSlider;
    private static float fresnelSlider;
    private static Vector2 position;

    private static int typeDropDown;
    private static int cityDropDown;

    private static float output;

    private const float panelTilt = 21f;
    private float windowColor;

    private static float[,] panels = new float[3,2] {
        {350f, 0.005f},     // Monocrystal
        {300f, 0.004f},     // Polycrystal
        {125f, 0.0025f}};   // Thin layer

    private static float[,] places = new float[15,3] { // Radiance, latitute, longitude / CITY
        {3.8f, 49.1951f, 16.6068f},    // Brno
        {2.9f, 51.5074f, -0.1278f},    // London
        {4.4f, 40.7128f, -74.0060f},   // New York
        {4.3f, 35.6762f, 139.6503f},   // Tokyo
        {2.2f, 59.9139f, 10.7522f},    // Oslo
        {5.5f, 19.4326f, -99.1332f},   // Mexico City
        {5.3f, 19.0760f, 72.8777f},    // Mumbai
        {4.8f, -1.2921f, 36.8219f},    // Nairobi
        {4.9f, -22.9068f, -43.1729f},  // Rio de Janeiro
        {4.3f, 39.9042f, 116.4074f},  // Beijing
        {4.9f, -33.8688f, 151.2093f},  // Sydney
        {5.7f, 33.5731f, -7.5898f},    // Casablanca
        {3.9f, 49.2827f, -123.1207f},  // Vancouver
        {5.3f, -33.9249f, 18.4241f},   // Cape Town
        {2.4f, 60.1699f, 24.9384f}      // Helsinki
    };

    // Start is called before the first frame update
    void Start()
    {
        typeDropDown = 0;
        fresnelSlider = 1f;
        sunAngleSlider = 4f;
        output = 0;
        azimuthSlider = 70f;
        windowColor = 0.0f;
        Color col = new Color(windowColor, windowColor, 0f, 1f);
        mat.SetColor("_Color", col);
        mat.SetColor("_Emmision", col);
        }

    // Update is called once per frame
    void Update()
    {
        calculateOutput();
    }

    public static float[,] GettCiteis(){return places;}

    public static float GetSAS(){return sunAngleSlider;}
    public static void SetSAS(float angle){sunAngleSlider=angle;}

    public static float GetAZ(){return azimuthSlider;}
    public static void SetAZ(float angle){azimuthSlider=angle;}

    public static float GetCS(){return cloudSlider;}
    public static void SetCS(float val){cloudSlider=val;}

    public static float GetTS(){return tempSlider;}
    public static void SetTS(float val){tempSlider=val;}

    public static float GetFS(){return fresnelSlider;}
    public static void SetFS(float val){fresnelSlider=val;}

    public static int GetTDD(){return typeDropDown;}
    public static void SetTDD(int val){typeDropDown=val;}

    public static int GetCDD(){return cityDropDown;}
    public static void SetCDD(int val){cityDropDown=val;}

    public static Vector2 GetPosition(){return new Vector2(places[cityDropDown,1], places[cityDropDown,2]);}
    public static void SetPosition(Vector2 val){position = val;}

    public static float GetOutput(){return output;}

    private float calcSunlightHours(float lat){
        float totalHours = 0f;
        float equatorSunHours = 2080;

        for(int i = 1; i <= 365; i++){
            float inAcos = -Mathf.Tan(lat) * Mathf.Tan(23.45f * Mathf.Sin(360f * ((284 + i)/365f)));
            if(inAcos>1f){inAcos=1f;};
            if(inAcos<-1f){inAcos=-1f;};
            totalHours += 2 * ((1f/15f) * Mathf.Acos(inAcos));
        }
        return totalHours;

    }

    private float calcZenithAngle(float lat, float longitude){
        float zenithAngle;
        float sunDeclin;
        float sunHourAngle;

        // Sun declination
        float sum = 0;
        for(int i = 1; i <= 365; i++){
            sum += 23.45f * (2 * Mathf.PI * (284 + i) / 365f);
        }
        sunDeclin = sum / 365f;

        /// Hour angle of the Sun
        // LaT
        float locSolTimeNoon;
        float eqOfTime = 0;

        for(int i = 1; i <= 365; i++){
            float beta = (2 * Mathf.PI * (i - 81))/365;
            eqOfTime += 9.87f*Mathf.Sin(2*beta) - 7.53f*Mathf.Cos(beta) - 1.5f*Mathf.Sin(beta);
        }
        eqOfTime = eqOfTime /365f;
        locSolTimeNoon = 12f + ( longitude/15f) + eqOfTime;

        float observerTimeZoneDiff = longitude%15;


        /// Kalkulace z cerveneho screenu
        sunHourAngle = (((locSolTimeNoon - 12) * Mathf.PI)/12) + ((observerTimeZoneDiff * Mathf.PI) / 180f);

        float radLat = Mathf.Deg2Rad * lat;
        float radSunDec = Mathf.Deg2Rad * sunDeclin;
        float radHourAngle = Mathf.Deg2Rad * sunHourAngle;

        float sinZenith = Mathf.Rad2Deg * Mathf.Asin(Mathf.Sin(radLat)*Mathf.Sin(radSunDec) + Mathf.Cos(radLat)
            * Mathf.Cos(radSunDec) * Mathf.Cos(radHourAngle));

        return sinZenith;
    }

    private float calcAnnAvgSolRad(float lat, float longitude, float tilt, float azimuth){
        float n = 1f; // Total hours of sunlight
        float h = 1f;
        float z = 1f;

        n = calcSunlightHours(lat);
        z = calcZenithAngle(lat, longitude);

        n = 12 * 365f;

        //h = (float)(0.000007f * n * z * (float)(Mathf.Cos(lat) * Mathf.Cos(tilt) * Mathf.Sin(azimuth)
        //     + (Mathf.PI/180f) * azimuth * Mathf.Sin(lat) * Mathf.Sin(tilt))) * (1f - 0.1f*Mathf.Exp(-n/800f));

        h = (n / 8760f) * (Mathf.Cos(z) * Mathf.Sin(tilt) * Mathf.Cos(lat) * Mathf.Cos(azimuth - 180f)
             + Mathf.Sin(z) * Mathf.Cos(tilt) * Mathf.Cos(lat));
        return h;
    }

    private float calcFresnelLosses(){
        float nLen = 1.51f;
        return 1f - ((nLen - 1f)/(nLen + 1f))*((nLen - 1f)/(nLen + 1f));
    }

    private void calculateOutput(){

        float area = sunAngleSlider/4f;
        float pr = 0.95f; // Obecne ztraty na meterialech
        float r = panels[typeDropDown,0]/1000f; // kWp   WIP
        float h = places[cityDropDown,0]*1000f/3.0f;//calcAnnAvgSolRad(position.y, position.x, panelTilt, azimuthSlider);

        float tempVar = 1f - panels[typeDropDown,1] * tempSlider;
        pr *= tempVar * (1f - (cloudSlider * 0.9f));

        output = h*area*pr*r*fresnelSlider;
        if(fresnelSlider>1f){
            output *= calcFresnelLosses();
        }

        windowColor = output/800f;
        Color col = new Color(windowColor, windowColor, 0f, 1f);
        mat.SetColor("_Color", col);
        mat.SetColor("_Emmision", col);
        
    }

}

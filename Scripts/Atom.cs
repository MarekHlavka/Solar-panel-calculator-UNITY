using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{

    public float radius;
    // Start is called before the first frame update
    void Start()
    {   
        radius = 5.0f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(5,5,5), radius);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement to point
    }

    public void moveToDirection(Vector3 dir){
        transform.position += dir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diode : MonoBehaviour
{
    public Atom src;
    public GameObject field;

    private Vector3 dimensions;
    private Vector3 offset;

    List<Atom> atoms = new List<Atom>();

    // Start is called before the first frame update
    void Start()
    {   
        field = GameObject.Find("Inside");
        dimensions = field.transform.localScale;
        offset = field.transform.position;

        Atom first = Instantiate(src);
        first.transform.position = new Vector3(10,1,10);
        atoms.Add(first);

        Atom second = Instantiate(src);
        second.transform.position = new Vector3(10,3,10);
        atoms.Add(second);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Atom atom in atoms){
            atom.moveToDirection(new Vector3(0,0.1f,0));
        }
    }
}

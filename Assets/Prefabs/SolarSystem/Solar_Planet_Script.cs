using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class Solar_Planet_Script : MonoBehaviour
{
    public int index;
    public float radius;
    public float angle;

    public GameObject planet;
    public GameObject center;

    public int segments_number = 360;
    
    public void Ustawianie(int i, float r, float a, float tiltX=0, float tiltZ=0)
    {
        this.index = i;
        this.radius = r;
        this.angle = a;

        this.transform.rotation = Quaternion.Euler(tiltX, 0, tiltZ);
    }

    void Start()
    {
        int points_number = segments_number + 1;

        this.name = "Planet_"+index.ToString();
        //radius = Random.Range(0.2f, 9f);
        //angle = Random.Range(0.0f, 360.0f);
        planet.transform.Translate(radius, 0, 0);
        

        //GetComponent<LineRenderer>().loop = true;
        GetComponent<LineRenderer>().useWorldSpace = false;
        GetComponent<LineRenderer>().positionCount = segments_number;

        Vector3[] points_position = new Vector3[segments_number];

        for (int i = 0; i < segments_number; i++)
        {
            float a_angle = Mathf.Deg2Rad * (i * 360f / segments_number);
            points_position[i] = new Vector3(radius * Mathf.Sin(a_angle), 0, radius * Mathf.Cos(a_angle));
        }

        GetComponent<LineRenderer>().SetPositions(points_position);
        //CreateOrbit();
        
        center.transform.Rotate(0, angle, 0);
    }

    void Update()
    {
        
    }
}

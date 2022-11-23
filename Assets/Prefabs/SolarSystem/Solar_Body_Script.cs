using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar_Body_Script : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float zradius;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreateOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateOrbit()
    {
        float x;
        float y = 0f;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}

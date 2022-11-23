using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solar_System_Script : MonoBehaviour
{
    public GameObject star;
    public bool random_planets_number;
    public int planet_number;

    public float minRange = 0.2f;
    public float maxRange = 50f;
    public float minTheta = 0.0f;   //raczej nie dotykac
    public float maxTheta = 360.0f; //raczej nie dotykac

    public GameObject planet;
    public int bodies_number;
    public GameObject body;

    private List<string> SolarSysContent = new List<string>();

    public struct OrbitParams
    {
        public float radius;
        public float theta;
    };

    void Start()
    {
        NowySolarSys();
    }

    void Update()
    {
    }

    public void NowySolarSys()
    {
        Instantiate(star, Vector3.zero, Quaternion.identity);
        SolarSysContent.Add("Solar_Star_Pref(Clone)");

        // Planets
        if (random_planets_number)
        {
            planet_number = (int)Random.Range(0, planet_number);
        }

        //OrbitParams[] orbityTab = new OrbitParams[planet_number];
        List<OrbitParams> orbityList = new List<OrbitParams>();
        //OrbitParams current_orbit;

        for (int planet_index = 0; planet_index < planet_number; planet_index++)
        {
            orbityList.Add(new OrbitParams
            {
                radius = Random.Range(minRange, maxRange),
                theta = Random.Range(minTheta, maxTheta)
            });

            //Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>().index = planet_index;
        }

        //orbityList.Sort();
        orbityList.Sort(delegate (OrbitParams x, OrbitParams y) {
            return x.radius.CompareTo(y.radius);
        });

        for (int planet_index = 0; planet_index < planet_number; planet_index++)
        {
            Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>().Ustawianie(planet_index + 1, orbityList[planet_index].radius, orbityList[planet_index].theta);
            //curr_planet = Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>();
            SolarSysContent.Add("Planet_" + (planet_index + 1));
        }

        // DEBUG ONLY !!
        //foreach (string x in SolarSysContent)
        //{
        //    Debug.Log(x);
        //}
    }

    public void ZniszczSolarSys()
    {
        foreach (string GameObjectsName in SolarSysContent)
        {
            Destroy(GameObject.Find(GameObjectsName), 0.0f);
        }

        SolarSysContent.Clear();
    }
}

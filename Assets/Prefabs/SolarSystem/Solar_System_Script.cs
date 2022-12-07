using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solar_System_Script : MonoBehaviour
{
    public GameObject star;
    public bool random_planets_number;
    public int public_planet_number;
    private int planet_number;

    public float minRange = 0.5f;
    public float maxRange = 50f;
    private float minTheta = 0.0f;   //const, nie dotykac
    private float maxTheta = 360.0f; //const, nie dotykac
    public float minTilt = -2.5f;
    public float maxTilt = 2.5f;

    public GameObject planet;
    public int bodies_number;
    private
        Transform parent_transform;
    public GameObject parent_gameobject;

    private List<string> SolarSysContent = new List<string>();
    public bool NieMaPlanet;

    public struct OrbitParams
    {
        public float radius;
        public float theta;
        public float tiltX;
        public float tiltZ;
    };

    void Start()
    {
        parent_transform = parent_gameobject.transform;
        NowySolarSys();
    }

    void Update()
    {
    }

    public void NowySolarSys()
    {
        ZniszczSolarSys();
        //if (SolarSysContent.Count > 0)
        //{
        //    ZniszczSolarSys();
        //}
        //if (parent.transform.childCount > 0)
        //{
        //    ZniszczSolarSys();
        //}

        GameObject instancja;

        instancja = Instantiate(star, Vector3.zero, Quaternion.identity);
        //instancja.name = "Solar_Star_Pref(Clone)";
        instancja.name = "Main_Star";
        instancja.transform.SetParent(parent_transform, true);
        //SolarSysContent.Add("Solar_Star_Pref(Clone)");

        // Planety
        if (random_planets_number) 
        { 
            planet_number = (int)Random.Range(0, public_planet_number); 
        }
        else 
        { 
            planet_number = public_planet_number; 
        }

        if (planet_number == 0)
        {
            //NieMaPlanet = true;
            return;  //bo nie ma co stworzyc
        }
        //else 
        //{ 
        //    NieMaPlanet = false; 
        //}
        
        List<OrbitParams> orbityList = new List<OrbitParams>();
        //OrbitParams current_orbit;

        for (int planet_index = 0; planet_index < planet_number; planet_index++)
        {
            orbityList.Add(new OrbitParams
            {
                radius = Random.Range(minRange, maxRange),
                theta = Random.Range(minTheta, maxTheta),
                tiltX = Random.Range(minTilt, maxTilt),
                tiltZ = Random.Range(minTilt, maxTilt)
            });

            //Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>().index = planet_index;
        }

        orbityList.Sort(delegate (OrbitParams x, OrbitParams y) { return x.radius.CompareTo(y.radius); });

        

        for (int planet_index = 0; planet_index < planet_number; planet_index++)
        {
            //Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>().Ustawianie(planet_index + 1, orbityList[planet_index].radius, orbityList[planet_index].theta, orbityList[planet_index].tiltX, orbityList[planet_index].tiltZ);
            instancja = Instantiate(planet, Vector3.zero, Quaternion.identity);
            instancja.GetComponent<Solar_Planet_Script>().Ustawianie(planet_index + 1, orbityList[planet_index].radius, orbityList[planet_index].theta, orbityList[planet_index].tiltX, orbityList[planet_index].tiltZ);
            instancja.name = "Planet_" + (planet_index + 1);
            instancja.transform.SetParent(parent_transform, true);

            //curr_planet = Instantiate(planet, Vector3.zero, Quaternion.identity).GetComponent<Solar_Planet_Script>();
            //SolarSysContent.Add("Planet_" + (planet_index + 1));
            Debug.Log("- Stworzenie: Planet_" + (planet_index + 1));    //DEBUG ONLY!!
        }
    }

    public void ZniszczSolarSys()
    {
        //Destroy(GameObject.Find("Solar_Star_Pref(Clone)"), 0.0f);

        //if (!NieMaPlanet)
        //{
        //    foreach (string GameObjectsName in SolarSysContent) 
        //    {
        //        //Debug.Log("- Niszczenie: "+GameObjectsName);    //DEBUG ONLY!!
        //        Destroy( GameObject.Find(GameObjectsName), 0.0f ); 
        //    }
        //}

        //SolarSysContent.Clear();

        for (var i = parent_gameobject.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(parent_gameobject.transform.GetChild(i).gameObject);
        }
    }
}

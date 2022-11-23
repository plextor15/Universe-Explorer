using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars_Particles_Spiral_Script : MonoBehaviour
{
    public GameObject Promien;
    public GameObject DebugMarker;
    //public GameObject GalaxyKamera; //odleglosc od galaktyki

    ParticleSystem.Particle p;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    public bool moveLock = false;    //blokada na poruszanie
    public float speed;

    public void Raying(bool x)
    {
        Debug.Log("Raying(): " + x);

        if (x)
        {
            Promien.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            Promien.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    void Start()
    {
       
    }

    void OnEnable()
    {
        Debug.Log("-- Testowe OnEnable() --");
        ps = GetComponent<ParticleSystem>();
        Debug.Log(ps);
    }

    void Update()
    {
        if (true) //jesli aktuanie w Layer.Stars
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Star Level Ray Shoot");
                Raying(true);
            }
            else
            {
                Raying(false);
            }
        }
    }

    void OnParticleTrigger()
    {
        Debug.Log("-- ParticleTrigger() --");

        if (ps == null)
        {
            Debug.LogError("Could not find a particle system");
        }

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if (numEnter == 0)
        {
            return;
        }

        DebugMarker.transform.position = enter[0].position;
        Debug.Log("poz: " + p.position);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
            Debug.Log(other.tag);
    }


    public void Star_Reset() 
    { 
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
    }
}

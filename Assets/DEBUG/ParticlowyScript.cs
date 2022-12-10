using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParticlowyScript : MonoBehaviour
{
    public GameObject OdParticli;
    public int IleParticli; //max 9605

    private ParticleSystem PS;
    private bool Niezmienione = true;

    void Start()
    {
        PS = OdParticli.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Niezmienione)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[10000];
            int ileZebranych = PS.GetParticles(particles);
            Debug.Log("Ile zebranych"+ ileZebranych);
            //Debug.Log("od ostat FPS: "+ Time.deltaTime);

            if (ileZebranych == IleParticli)
            {
                for (int i = 0; i < ileZebranych; i++)
                {
                    particles[i].position = new Vector3((i * 0.5f), 0, 0);
                    //particles[i].rotation3D = new Vector3((i * 0.2f), 0, 0);
                    //particles[i].startSize3D = Random.Range(0.5f, 2f) * Vector3.one;
                }
                PS.SetParticles(particles, ileZebranych);
                Niezmienione = false;
            }
        }
    }
}

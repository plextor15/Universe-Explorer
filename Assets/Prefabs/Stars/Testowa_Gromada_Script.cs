using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testowa_Gromada_Script : MonoBehaviour
{
    public GameObject Promien;
    
    public GameObject DebugMarker;
    public bool CzyRaying = false;
    private ParticleSystem ps;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    ParticleSystem.Particle p;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("-- Testowe OnEnable() --");
        ps = GetComponent<ParticleSystem>();
        //Debug.Log(ps);
    }

    void OnParticleTrigger()
    {
        Debug.Log("-- ParticleTrigger() --");
        //int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //DebugMarker.transform.position = enter[0].position;
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        
        if (ps == null)
        {
            Debug.LogError("Could not find a particle system");
        }

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //Enter
        if (numEnter == 0)
        {
            return;
        }

        //p = enter[0];
        //p.startColor = new Color32(255, 0, 0, 255);
        //enter[0] = p;
        DebugMarker.transform.position = enter[0].position;
        Debug.Log("poz: " + p.position);

        
        //Exit
        //if (numExit == 0)
        //{
        //    return;
        //}

        //p = exit[0];
        //p.startColor = new Color32(255, 255, 255, 255);
        //exit[0] = p;
        //Debug.Log("poz: " + p.position);

        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    public void Raying(bool x)
    {
        //DEBUG ONLY!!
        x = CzyRaying;
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
}

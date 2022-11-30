using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPlayer_Script : MonoBehaviour
{
    public enum Warstwy
    {
        None = 0,
        CelestialBody = 1,
        SolarSys = 2,
        Stars = 3,
        Galaxy = 4
    }

    public GameObject Stars;

    //Kamery od warstw
    public GameObject Camera_Galaxy;
    public GameObject Camera_Stars;
    public GameObject Camera_Sol;
    public GameObject Camera_Body;

    //Prefaby od warstw
    //public GameObject Prefab_Galaxy;
    //public GameObject Prefab_Stars;
    public GameObject Prefab_SolarSys;
    public GameObject Prefab_CelestialBdoy;


    public float sensitivity;
    public float roll_rate;

    private float mouse_Y; //Mouse Y
    private float mouse_X; //Mouse X

    public Warstwy currentLayer;
    public Warstwy previousLayer;
    public float speed;
    public float multipl = 1.25f;
    public float multiplBoost = 1000f;
    public GameObject Stars_Pref;

    //UI
    public GameObject HUD;
    public Text Speed_UI;
    public Text Warstwa_UI;

    public GameObject help;


    void Start()
    {
        RotKamer();//ustawianie rotacji wszystkich kamer
        Zmiana_Warswy(Warstwy.SolarSys);
    }

    void Update()
    {
        Movement();
        DebugKeys();
    }

    void Movement()
    {
        //Celowanie
        if (Input.GetKey(KeyCode.Mouse1))
        {
            mouse_Y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            mouse_X = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            this.transform.Rotate(Vector3.up, mouse_X, Space.Self);
            this.transform.Rotate(Vector3.right, mouse_Y, Space.Self);

            RotKamer();//ustawianie rotacji wszystkich kamer
        }

        //Roll
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(Vector3.forward, -roll_rate * Time.deltaTime, Space.Self);

            RotKamer();//ustawianie rotacji wszystkich kamer
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.forward, roll_rate * Time.deltaTime, Space.Self);

            RotKamer();//ustawianie rotacji wszystkich kamer
        }

        
        //Speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ChangeSpeed(Input.mouseScrollDelta.y * multipl * multiplBoost);
        }
        else
        {
            ChangeSpeed(Input.mouseScrollDelta.y * multipl);
        }


        //WSAD
        if (Input.GetKey("w"))
        {
            TranslateKamer(Vector3.forward);
        }

        if (Input.GetKey("s"))
        {
            TranslateKamer(Vector3.back);
        }

        if (Input.GetKey("d"))
        {
            TranslateKamer(Vector3.right);
        }

        if (Input.GetKey("a"))
        {
            TranslateKamer(Vector3.left);
        }

        //Funkcyjne
        if (Input.GetKey("r")) //reset speed
        {
            ChangeSpeed(Mathf.NegativeInfinity);
        }

        //Kazda kamera ma wlasny Raying()
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Camera_Sol.GetComponent<CameraSol_Script>().Raycasting();
        //}

        if (Input.GetKeyDown("h")) //help
        {
            if (help.activeInHierarchy)
            { 
                help.SetActive(false);
            }
            else 
            { 
                help.SetActive(true); 
            }
        }
    }

    void RotKamer() //ustawianie rotacji wszystkich kamer
    {
        Camera_Galaxy.transform.rotation = this.transform.rotation;
        Camera_Stars.transform.rotation = this.transform.rotation;
        Camera_Sol.transform.rotation = this.transform.rotation;
        Camera_Body.transform.rotation = this.transform.rotation;
    }

    void TranslateKamer(Vector3 kierunek)
    {
        if (currentLayer == Warstwy.Galaxy)
        {
            Camera_Galaxy.transform.Translate(kierunek * speed * Time.deltaTime, Space.Self);
        }
        if (currentLayer == Warstwy.Stars)
        {
            //Camera_Galaxy.transform.Translate(kierunek * speed * Time.deltaTime / 10000, Space.Self);
            //Camera_Stars.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            Camera_Stars.transform.Translate(kierunek * speed * Time.deltaTime, Space.Self);
        }
        if (currentLayer == Warstwy.SolarSys)
        {
            Camera_Sol.transform.Translate(kierunek * speed * Time.deltaTime, Space.Self);
        }
        if (currentLayer == Warstwy.CelestialBody)
        {
            Camera_Body.transform.Translate(kierunek * speed * Time.deltaTime, Space.Self);
        }
    }

    public void Zmiana_Warswy(Warstwy w)
    {
        if (w == Warstwy.CelestialBody)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.CelestialBody;

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;

            //Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().moveLock = true;
            Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = true;
            Camera_Body.GetComponent<Camera>().enabled = true;

            Warstwa_UI.text = currentLayer.ToString();
        }

        if (w == Warstwy.SolarSys)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.SolarSys;

            //Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().moveLock = true;
            Debug.Log("Warstwa: " + currentLayer);

            //Camera_Sol skierowana na gwiazde i w odpowiedniej odleglosci
            if (previousLayer != Warstwy.CelestialBody)
            {
                Camera_Sol.transform.position = Vector3.zero;
                Camera_Sol.transform.rotation = Camera_Stars.transform.rotation;
                Camera_Sol.transform.Translate(Vector3.back * 10, Space.Self);
            }

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;

            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = true;
            Camera_Body.GetComponent<Camera>().enabled = false;

            Warstwa_UI.text = currentLayer.ToString();
        }

        if (w == Warstwy.Stars)
        {
            //if (currentLayer == Warstwy.Galaxy)
            //{
            //    Stars.GetComponent<ParticleSystem>().Clear();
            //    Stars.GetComponent<ParticleSystem>().Play();
            //}

            previousLayer = currentLayer;
            currentLayer = Warstwy.Stars;

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = true;

            //Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().moveLock = false;
            Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = false;
            Camera_Body.GetComponent<Camera>().enabled = false;

            Warstwa_UI.text = currentLayer.ToString();
        }

        if (w == Warstwy.Galaxy)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.Galaxy;

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;

            //Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().moveLock = true;
            Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = false;
            Camera_Sol.GetComponent<Camera>().enabled = false;
            Camera_Body.GetComponent<Camera>().enabled = false;

            Warstwa_UI.text = currentLayer.ToString();
        }
    }

    public void ChangeSpeed(float deltaSpeed) 
    {
        this.speed = this.speed + deltaSpeed;
        if (speed < 0)
        {
            speed = 0;
        }

        //Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().speed = speed;

        Speed_UI.text = speed.ToString() + " units / s";

    }

    void DebugKeys()
    {
        //  Warstwy manualne przechodzenie (numery jak w enum) ----- DEBUG ONLY!!
        if (Input.GetKey("1")) Zmiana_Warswy(Warstwy.CelestialBody);
        if (Input.GetKey("2")) Zmiana_Warswy(Warstwy.SolarSys);
        if (Input.GetKey("3")) Zmiana_Warswy(Warstwy.Stars);
        if (Input.GetKey("4")) Zmiana_Warswy(Warstwy.Galaxy);

        if (Input.GetKey("5")) // destroy & exit solar sys
        {
            Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();
            Zmiana_Warswy(Warstwy.Stars);
        }

        if (Input.GetKey("6")) // destroy & create solar sys
        {
            Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();
            Prefab_SolarSys.GetComponent<Solar_System_Script>().NowySolarSys();
            Zmiana_Warswy(Warstwy.SolarSys);
        }
        
        if (Input.GetKey("0")) Stars_Pref.GetComponent<Stars_Particles_Spiral_Script>().Star_Reset();
    }
}
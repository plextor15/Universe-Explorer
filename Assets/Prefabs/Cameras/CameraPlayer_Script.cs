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

    //public GameObject Stars;

    //Kamery od warstw
    public GameObject Camera_Galaxy;
    public GameObject Camera_Stars;
    public GameObject Camera_Sol;
    public GameObject Camera_Body;

    //Prefaby od warstw
    //public GameObject Prefab_Galaxy;
    public GameObject Prefab_SpiralStars;
    public GameObject Prefab_EliptStars;
    public GameObject Prefab_SpawnerGromad;
    public GameObject Prefab_SolarSys;
    public GameObject Prefab_CelestialBdoy;

    
    public float sensitivity;
    public float roll_rate;

    private float mouse_Y; //Mouse Y
    private float mouse_X; //Mouse X

    public Warstwy currentLayer;
    public Warstwy previousLayer;
    public Warstwy odSlidera;
    private bool pendingLayer = false;
    public float speed;
    public float multipl = 0.01f;
    //public float multiplBoost = 1000f;
    //public GameObject Stars_Pref;

    //UI
    public GameObject HUD;
    public GameObject Slider_script;
    public Slider SliderComponent;
    public Text Speed_UI;
    private string SpeedJednostki = " unit/s";
    public Text Warstwa_UI; //Juz nie uzywane

    public GameObject help;


    void Start()
    {
        RotKamer();//ustawianie rotacji wszystkich kamer
        Zmiana_Warswy(Warstwy.Stars);

        SliderSetSpeed(1.25f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); //Wyjscie

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
            RotKamer(); //ustawianie rotacji wszystkich kamer
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.forward, roll_rate * Time.deltaTime, Space.Self);
            RotKamer(); //ustawianie rotacji wszystkich kamer
        }

        //
        //      !!! PREDKOSC TYLKO ZA POMOCA SLIDERA !!!
        //                  Zmiana przez slidera
        //
        //Speed
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    ChangeSpeed(Input.mouseScrollDelta.y * multipl * multiplBoost);
        //}
        //else
        //{
        //    ChangeSpeed(Input.mouseScrollDelta.y * multipl);
        //}
        if (Input.mouseScrollDelta.y != 0)
        {
            float s = SliderComponent.value;
            s = s + Input.mouseScrollDelta.y * multipl;
            SliderComponent.value = s;
            SliderSetSpeed(s);
            //Debug.Log(Input.mouseScrollDelta.y);
        }

        //WSAD
        //float sliderVal = SliderComponent.value; //w Unity WASD zmienia slider automatycznie

        //zmiana warstwy z Speed Slidera
        if (pendingLayer)
        {
            if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Zmiana_Warswy(odSlidera);
                pendingLayer = false;
                odSlidera = Warstwy.None;
            }
        }

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

        //SliderComponent.value = sliderVal;

        //Funkcyjne

        //
        //      !!! PREDKOSC TYLKO ZA POMOCA SLIDERA !!!
        //                  Zmiana przez slidera
        //
        //if (Input.GetKey("r")) //reset speed jest w Sliderze od predkosci
        //{
        //    ChangeSpeed(Mathf.NegativeInfinity);
        //}

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
            Camera_Galaxy.transform.Translate((kierunek * speed * Time.deltaTime)/100, Space.Self);
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
        if (w == Warstwy.None)
        {
            return;
        }

        if (w == Warstwy.CelestialBody)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.CelestialBody;

            Camera_Body.transform.rotation = Camera_Sol.transform.rotation;
            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;
            Prefab_CelestialBdoy.SetActive(true);
            Prefab_CelestialBdoy.transform.localScale = Vector3.one;

            //Wycentrowanie
            Camera_Body.transform.position = Vector3.zero;
            Camera_Body.transform.rotation = Camera_Sol.transform.rotation;
            Camera_Body.transform.Translate(Vector3.back * 40f, Space.Self);

            //Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = true;
            Camera_Body.GetComponent<Camera>().enabled = true;

            //Warstwa_UI.text = currentLayer.ToString();
            Slider_script.GetComponent<HUDSlider_Script>().UstawZnacznik(currentLayer);
            SliderComponent.value = 0.9f;
            SliderSetSpeed(0.9f);
        }

        if (w == Warstwy.SolarSys)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.SolarSys;

            Prefab_CelestialBdoy.SetActive(false);
            Prefab_CelestialBdoy.transform.localScale = Vector3.zero;

            //Camera_Sol skierowana na gwiazde i w odpowiedniej odleglosci
            if (previousLayer != Warstwy.CelestialBody)
            {
                Camera_Sol.transform.position = Vector3.zero;
                Camera_Sol.transform.rotation = Camera_Stars.transform.rotation;
                Camera_Sol.transform.Translate(Vector3.back * 10f, Space.Self);

                //Niszczenie+Tworzenie SolarSys
                //Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();
                Prefab_SolarSys.GetComponent<Solar_System_Script>().NowySolarSys();
                
                SliderComponent.value = 1.7f;
                SliderSetSpeed(1.7f);
            }
            else 
            {
                if (SliderComponent.value < 1.0f)
                {
                    SliderComponent.value = 1.2f;
                }
                SliderSetSpeed(SliderComponent.value);
            }

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;

            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = true;
            Camera_Body.GetComponent<Camera>().enabled = false;

            //Warstwa_UI.text = currentLayer.ToString();
            Slider_script.GetComponent<HUDSlider_Script>().UstawZnacznik(currentLayer);
        }

        if (w == Warstwy.Stars)
        {
            
            // TO JEST ROBIONE W SPIRAL/ELIPT_PARTICLE_SCRIPT
            //if (currentLayer == Warstwy.Galaxy)
            //{
            //    Camera_Stars.transform.position = new Vector3(  Camera_Galaxy.transform.position.x * 100f, 
            //                                                    Camera_Galaxy.transform.position.y * 100f, 
            //                                                    Camera_Galaxy.transform.position.z * 100f);
            //}

            previousLayer = currentLayer;
            currentLayer = Warstwy.Stars;

            Camera_Galaxy.GetComponent<Camera>().nearClipPlane = 10f;
            
            if (previousLayer == Warstwy.SolarSys)
            {
                Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();

                //SliderComponent.value = 2.0f;
                //SliderSetSpeed(2.0f);

                if (SliderComponent.value < 2.0f)
                {
                    SliderComponent.value = 2.0f;
                }
                SliderSetSpeed(SliderComponent.value);
            }
            else 
            {
                //Prefab_CelestialBdoy.SetActive(true); // Elipt/Spiral_Particles_Script sie tym zajmuja

                SliderComponent.value = 2.8f;
                SliderSetSpeed(2.8f);
            }

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = true;
            Prefab_CelestialBdoy.SetActive(false);

            //Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = true;
            Camera_Sol.GetComponent<Camera>().enabled = false;
            Camera_Body.GetComponent<Camera>().enabled = false;

            //Warstwa_UI.text = currentLayer.ToString();
            Slider_script.GetComponent<HUDSlider_Script>().UstawZnacznik(currentLayer);
        }

        if (w == Warstwy.Galaxy)
        {
            previousLayer = currentLayer;
            currentLayer = Warstwy.Galaxy;

            Prefab_EliptStars.SetActive(false);
            Prefab_SpiralStars.SetActive(false);

            Camera_Galaxy.GetComponent<Camera>().nearClipPlane = 0.1f;
            Prefab_SpawnerGromad.SetActive(false);

            Camera_Stars.GetComponent<CameraStars_Script>().CzyWarstwaStars = false;
            Prefab_CelestialBdoy.SetActive(false); //jakiekolwiek by nie byly, to sa wszystkie niszczone

            //Debug.Log("Warstwa: " + currentLayer);
            Camera_Galaxy.GetComponent<Camera>().enabled = true;
            Camera_Stars.GetComponent<Camera>().enabled = false;
            Camera_Sol.GetComponent<Camera>().enabled = false;
            Camera_Body.GetComponent<Camera>().enabled = false;

            //Warstwa_UI.text = currentLayer.ToString();
            Slider_script.GetComponent<HUDSlider_Script>().UstawZnacznik(currentLayer);

            if (SliderComponent.value < 3.0f)
            {
                SliderComponent.value = 3.2f;
            }
            SliderSetSpeed(SliderComponent.value);
        }
    }

    //
    //      !!! PREDKOSC TYLKO ZA POMOCA SLIDERA !!!
    //
    //public void ChangeSpeed(float deltaSpeed) 
    //{
    //    this.speed = this.speed + deltaSpeed;
    //    if (speed < 0)
    //    {
    //        speed = 0;
    //    }
    //    Speed_UI.text = speed.ToString() + SpeedJednostki;
    //}

    public void SliderSetSpeed(float x)
    {
        int w = (int)x;
        float s = x - w;
        //Debug.Log("Warstwa - "+w+", s = "+s);

        //jednostki sa tylko jako DEBUG!!
        switch (w)
        {
            case 0: //CelestialBody
                speed = Mathf.Lerp(0.1f, 15f, s);
                SpeedJednostki = " Mm/s";

                odSlidera = Warstwy.CelestialBody; 
                break;

            case 1: //SolarSys
                speed = Mathf.Lerp(1f, 30f, s);
                SpeedJednostki = " AU/s";

                odSlidera = Warstwy.SolarSys; 
                break;

            case 2: //Stars
                speed = Mathf.Lerp(5f, 100f, s);
                SpeedJednostki = " LY/s";

                odSlidera = Warstwy.Stars; 
                break;

            case 3: //Galaxy
                speed = Mathf.Lerp(2f, 150f, s);
                SpeedJednostki = " kLY/s";

                odSlidera = Warstwy.Galaxy; 
                break;

            default: break;
        }

        Speed_UI.text = speed.ToString("0.00") + SpeedJednostki;
        if (currentLayer != odSlidera)
        {
            if ((int)currentLayer < (int)odSlidera)
            {
                pendingLayer = true;
                //Zmiana_Warswy(odSlidera); // jest w Movement() WSAD
            }
        }
        else
        {
            pendingLayer = false;
        }
    }

    void DebugKeys()
    {
        //  Warstwy manualne przechodzenie (numery jak w enum) ----- DEBUG ONLY!!
        if (Input.GetKeyDown("1")) Zmiana_Warswy(Warstwy.CelestialBody);
        if (Input.GetKeyDown("2")) Zmiana_Warswy(Warstwy.SolarSys);
        if (Input.GetKeyDown("3")) Zmiana_Warswy(Warstwy.Stars);
        if (Input.GetKeyDown("4")) Zmiana_Warswy(Warstwy.Galaxy);

        if (Input.GetKeyDown("5")) // destroy & exit solar sys
        {
            Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();
            Zmiana_Warswy(Warstwy.Stars);
        }

        if (Input.GetKeyDown("6")) // destroy & create solar sys
        {
            Prefab_SolarSys.GetComponent<Solar_System_Script>().ZniszczSolarSys();
            Prefab_SolarSys.GetComponent<Solar_System_Script>().NowySolarSys();
            Zmiana_Warswy(Warstwy.SolarSys);
        }
    }
}
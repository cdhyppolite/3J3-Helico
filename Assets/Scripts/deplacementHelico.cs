using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deplacementHelico : MonoBehaviour
{
    /* Script qui gère l'hélicoptère
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */

    Rigidbody helicoRigid;
    //Vitesse----------------------------
    public float vitesseAvant = 0.00f;
    public float vitesseAvantMax = 10000f;
    public float vitesseTourne = 1000f;
    public float vitesseMonte = 1000f;
    //Variables pour une collision-------
    public static bool finJeu = false;
    public GameObject explosion;
    public AudioClip sonBidon;
    public GameObject carburant;
    public GameObject boutonRecommencer;
    public GameObject camera3;
    public Mesh helicoAccidente;
    public Mesh helice2Accidente;
    //Hélices-------------------------------
    public GameObject helice1;//arrière
    public GameObject helice2;//haut
    public static bool vitesseRotMin = false;
    //Drapeau---------------------------------
    public static bool drapeau;
    public GameObject msgDrapeau;
    //Particules--------------------
    public ParticleSystem particuleG;
    public ParticleSystem particuleD;
    //Matériau
    public Material couleurHelico;

    void Start()
    {
        drapeau = false; //Déactive la condition du drapeau
        helicoRigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
        //GetComponent<AudioSource>().Play();
        couleurHelico.color = Random.ColorHSV(0f, 1f, 1f,1f, 0.5529411f, 0.5529411f);

        /*float H, S, V;
        Color.RGBToHSV(couleurHelico.color, out H, out S, out V);
        Debug.Log("H: " + H + " S: " + S + " V: " + V);*/
        //Debug.Log("H: " + H*360 + " S: " + S*100 + " V: " + V*100);
    }
    void FixedUpdate()
    {
        float forceRotation = Input.GetAxis("Horizontal") * (vitesseTourne + vitesseAvant / 5);
        float forceMonter = Input.GetAxis("Vertical") * (vitesseMonte + vitesseAvant / 5);

        //Commande activer lorsque le moteur est en marche
        if ((tourneObjet.conditionMoteur == true) && (finJeu==false) && (vitesseRotMin==true))
        {
            helicoRigid.AddRelativeTorque(0, forceRotation, 0);
            helicoRigid.AddRelativeForce(0, forceMonter, 0);

            //augmenter la vitesse avant avec "e"
            if (Input.GetKey("e") && vitesseAvant < vitesseAvantMax)
            {
                vitesseAvant += 100;
            }
            //diminuer la vitesse avant avec "q"
            else if (Input.GetKey("q") && vitesseAvant >= 100)
            {
                vitesseAvant -= 100;
            }
            helicoRigid.AddRelativeForce(0, forceMonter, vitesseAvant);
        }

        //----------------------------------------------------------------------------
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
    void Update()
    {
        //print((tourneObjet.conditionMoteur) + "----" + (!finJeu) + "----" + (vitesseRotMin));

        //Gravité
        if ((tourneObjet.conditionMoteur == true) && (vitesseRotMin == true))
        {
            helicoRigid.useGravity = false;
            helicoRigid.drag = 1;
        }
        else
        {
            helicoRigid.useGravity = true;
            helicoRigid.drag = 0;
        }

        gestionSon();
        //print(helicoRigid.velocity.magnitude);
        //print(helicoRigid.velocity.x + " " + helicoRigid.velocity.y + " " + helicoRigid.velocity.z);
        //((helicoRigid.velocity.x*helicoRigid.velocity.x)+(helicoRigid.velocity.z*helicoRigid.velocity.z))*0.5
        //print(Mathf.Sqrt((helicoRigid.velocity.x * helicoRigid.velocity.x) + (helicoRigid.velocity.z * helicoRigid.velocity.x)));
    }
    void OnCollisionEnter(Collision infoObjet)
    {
        //Debug.Log(infoObjet.gameObject);
        if ((infoObjet.gameObject.tag == "terrain")|| (infoObjet.gameObject.tag == "drone"))
        {
            //print(infoObjet.gameObject.name
            detruireHelico();
        }
        /*if (infoObjet.gameObject.name == "Plateforme")
        {
            if (helicoRigid.velocity.magnitude > 10) //.velocity.y
            {
                //print("trop vite");
                detruireHelico();
            }
        }*/
    }
    void OnTriggerEnter(Collider infoObjet)
    {
        if (infoObjet.gameObject.tag == "bidon") //permet de récupérer de l'essence
        {
            GetComponent<AudioSource>().PlayOneShot(sonBidon);
            infoObjet.gameObject.SetActive(false);
            carburant.GetComponent<gestionEssence>().carburant += 20;

        }
        if (infoObjet.gameObject.name =="Drapeau") //Active un message lorsqu'on atteint le drapeau
        {
            infoObjet.gameObject.GetComponent<Collider>().enabled = false;
            msgDrapeau.gameObject.SetActive(true);
            drapeau = true;
        }
        if (infoObjet.gameObject.name == "cubeEau") //Particule près de l'eau
        {
            particuleG.gameObject.SetActive(true);
            particuleD.gameObject.SetActive(true);
        }
    }
    void OnTriggerStay(Collider infoObjet)
    {
        if (infoObjet.gameObject.name == "cubeEau") //Gère de nombre de particules
        {
            ParticleSystem particleSystemD = particuleD.GetComponent<ParticleSystem>();
            var emissionD = particleSystemD.emission;
            ParticleSystem particleSystemG = particuleG.GetComponent<ParticleSystem>();
            var emissionG = particleSystemG.emission;

            if (vitesseAvant == 0)
            {
                emissionD.rateOverTime = 0f;
                emissionG.rateOverTime = 0f;
                //particuleD.emission.rateOverTime = 0f; //Impossible de modifier la valeur comme ça. Il faut les autres variables.
            } else
            {
                emissionG.rateOverTime = (vitesseAvant* 400)/vitesseAvantMax;
                emissionD.rateOverTime = (vitesseAvant * 400) / vitesseAvantMax;
            }
        }

    }
    void OnTriggerExit(Collider infoObjet)
    {
        if (infoObjet.gameObject.name == "cubeEau") //Désactive les particules
        {
            particuleG.gameObject.SetActive(false);
            particuleD.gameObject.SetActive(false);
        }
    }
    void recommencer()
    {
        finJeu = false;
        SceneManager.LoadScene("SceneTerrain");
    }
    public void detruireHelico()
    {
        finJeu = true;
        //Change la forme
        GetComponent<MeshFilter>().mesh = helicoAccidente;
        helice2.GetComponent<MeshFilter>().mesh = helice2Accidente;
        //Changer la couleur de l'hélicoptère et des hélices
        helice1.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
        helice2.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
        GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
        //Activer l'objet explosion
        explosion.SetActive(true);
        Destroy(boutonRecommencer);
        Destroy(msgDrapeau);
        //Faire tomber l'hélicoptère
        tourneObjet.conditionMoteur = false;
        helicoRigid.freezeRotation = false;
        helicoRigid.angularDrag = 0;
        //Changer la caméra
        Camera.main.gameObject.SetActive(false);
        camera3.gameObject.SetActive(true);
        //Recommencer le jeu
        Invoke("recommencer", 8f);
    }
    void gestionSon() //modifie le son des hélices selon la vitesse de rotation
    {
        GetComponent<AudioSource>().volume = helice2.GetComponent<tourneObjet>().vitesseRotation.y / 50;
        GetComponent<AudioSource>().pitch = helice2.GetComponent<tourneObjet>().vitesseRotation.y / 50;

        if (GetComponent<AudioSource>().pitch < 0.5)
            GetComponent<AudioSource>().pitch = 0.5f;

        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
        } else
        {
            if(GetComponent<AudioSource>().isPlaying!=true)
            GetComponent<AudioSource>().Play();
        }
    }
}

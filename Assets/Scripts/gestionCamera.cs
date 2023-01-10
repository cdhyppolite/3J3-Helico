using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionCamera : MonoBehaviour
{
    /* Script qui permet de changer de caméra
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */
    public Camera camera1; //1ère personne
    public Camera camera2; //Surveillance
    public Camera camera3; //Fixe
    public Camera camera4; //3e personne
    private bool sonActive=true;
    void Update()
    {
        if (deplacementHelico.finJeu != true) //Ikpossible de changer si l'hélico est détruit
        {
            if (Input.GetKeyUp("1"))
            {
                Camera.main.gameObject.SetActive(false);
                camera1.gameObject.SetActive(true);
                activerSon();
            }

            if (Input.GetKeyUp("2"))
            {
                Camera.main.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
                activerSon();

            }

            if (Input.GetKeyUp("3"))
            {
                Camera.main.gameObject.SetActive(false);
                camera3.gameObject.SetActive(true);
                activerSon();
            }

            if (Input.GetKeyUp("4"))
            {
                Camera.main.gameObject.SetActive(false);
                camera4.gameObject.SetActive(true);
                activerSon();
            }
        }
        if (Input.GetKeyUp("m")) //Active ou coupe le son
        {
            sonActive = !sonActive;
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = !Camera.main.gameObject.GetComponent<AudioListener>().enabled;
        }
    }
    void activerSon()
    {
        if(sonActive == true)
        {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        } else
        {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }
}

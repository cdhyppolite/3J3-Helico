using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dome : MonoBehaviour
{
/* Script qui gère ouvre ou ferme le dome
*  programmeur: Carl-David Hyppolite
*  Date: 10 oct 2021
*/
    void Update()
    {
        if (Input.GetKeyUp("o"))
        {
            /*if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fermer") == true)
            {
                GetComponent<AudioSource>().Play();
            }*/
            GetComponent<Animator>().SetBool("ouvrir", !(GetComponent<Animator>().GetBool("ouvrir")));
        }
    }
    void sonOuverture()
    {
        GetComponent<AudioSource>().Play(); //s'active à l'ouverture ou fermeture.
    }
}

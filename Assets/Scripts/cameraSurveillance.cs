using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSurveillance : MonoBehaviour
{

    /* Script pour la cam�ra de surveillance
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */
    public GameObject cible;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cible.transform); // La cam�ra regarde sa cible en tout temps
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}

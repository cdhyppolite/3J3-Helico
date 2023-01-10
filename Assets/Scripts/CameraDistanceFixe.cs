using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceFixe : MonoBehaviour
{
    /* Script pour la caméra à distance fixe
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */

    public GameObject cible;
    public Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (cible.transform.position + distance); //transform.position = (position de la cible + la distance voulue en Vector3)
        transform.LookAt(cible.transform);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera3e : MonoBehaviour
{
/* Script pour la caméra à la 3e personne
*  programmeur: Carl-David Hyppolite
*  Date: 10 oct 2021
*/
    public GameObject cible;
    public Vector3 distaceCameraHelico;
    public float amortissement = 0.4f;
  

    // Déplace la caméra pour suivre l'hélico graduellement 
    void FixedUpdate()
    {
        // Définie une position 5 mètres en hauteur et 10 mètres en arrière de la cible (selon les axes locaux de la cible)
        var positionFinale = cible.transform.TransformPoint(distaceCameraHelico);  
        // prochaine position entre la position de départ de la caméra et la position finale désirée selon un facteur.5 
        transform.position = Vector3.Lerp(transform.position, positionFinale, amortissement);

        transform.LookAt(cible.transform);
    }

}


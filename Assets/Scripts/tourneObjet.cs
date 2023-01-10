using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourneObjet : MonoBehaviour
{
    /* Script qui permet de tourner les h�lices
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */

    public Vector3 vitesseRotation;
    public float acceleration;

    private bool demarreMoteur = false;
    public static bool conditionMoteur; //Il y a certains probl�me lorsque j'utilise demarraMoteur en static
    //-------------------------------//
    float tempX;
    float tempY;
    float tempZ;

    void Start()
    {
        //garder en m�moire les valeurs de vitesse
        tempX = vitesseRotation.x;
        tempY = vitesseRotation.y;
        tempZ = vitesseRotation.z;

        //Remettre les valeur � 0
        vitesseRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Ferme le moteur d�f�nitivement lorsque l'h�licopter br�le
        if (deplacementHelico.finJeu == true)
        {
            demarreMoteur = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
        }
        //�teint le moteur lorsqu'il n'y a plus d'essence
        if (gestionEssence.enPanne == true)
        {
            demarreMoteur = false;
        }

        //Active ou ferme le moteur.
        if ((Input.GetKeyUp(KeyCode.Return)) && (gestionEssence.enPanne==false))
        {
            if (deplacementHelico.finJeu == false)
            {
                if (demarreMoteur == false)
                {
                    demarreMoteur = true;
                }
                else
                {
                    demarreMoteur = false;
                }
            }    
        }

        //Active les h�lices lorsque le moteur est allum�
        if (demarreMoteur == true)
        {
            accelerationHelices(); //Effet d'acc�ration
        } 
        else
        {
            decelerationHelices(); //Ralentissement
        }

        //Determine si l'h�lice du haut tourne � 50% de la vitesse
        if (gameObject.name == "helice_haut")
        {
            if (vitesseRotation.y > tempY / 2)
            {
                deplacementHelico.vitesseRotMin = true;
            } else
            {
                deplacementHelico.vitesseRotMin = false;
            }
        }

        /*if (gameObject.name == "helice_haut")
        {
            print((tempY/2)+"----"+(vitesseRotation.y)+ "----" + (deplacementHelico.vitesseRotMin));
        }*/

        //Tourne H�lices
        gameObject.transform.Rotate(vitesseRotation);
        conditionMoteur = demarreMoteur;
    }

    void accelerationHelices()
    {
        //Permet aux h�lices d'acc�l�rer jusqu'� atteindre la vitesse max

        if ((vitesseRotation.x < tempX) && (vitesseRotation.x != tempX))
        {
            if ((vitesseRotation.x == 0) && (tempX != 0))
            {
                vitesseRotation.x = 0.25f;
            }
            vitesseRotation.x *= acceleration;
        }

        if ((vitesseRotation.y < tempY) && (vitesseRotation.y != tempY))
        {
            if ((vitesseRotation.y == 0) && (tempY!=0))
            {
                vitesseRotation.y = 0.25f;
            }

            vitesseRotation.y *= acceleration;
        }

        if ((vitesseRotation.z < tempY) && (vitesseRotation.z != tempZ))
        {
            if ((vitesseRotation.z == 0) && (tempZ != 0))
            {
                vitesseRotation.z = 0.25f;
            }
            vitesseRotation.z *= acceleration;
        }
    }

    void decelerationHelices()
    {
        //Ralentit les h�lices lorsque le moteur est �teint

        if (vitesseRotation.x > 0)
        {
            vitesseRotation.x /= acceleration;

            if (vitesseRotation.x < 1)
            {
                vitesseRotation.x = 0;
            }
        }

        if (vitesseRotation.y > 0)
        {
            vitesseRotation.y /= acceleration;

            if (vitesseRotation.y < 1)
            {
                vitesseRotation.y = 0;
            }
        }

        if (vitesseRotation.z > 0)
        {
            vitesseRotation.z /= acceleration;

            if (vitesseRotation.z < 1)
            {
                vitesseRotation.z = 0;
            }
        }
    }

}

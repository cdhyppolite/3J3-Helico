using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gestionEssence : MonoBehaviour
{
    /* Script qui gère le canvas (l'affichage du temps et le carburannt)
    *  programmeur: Carl-David Hyppolite
    *  Date: 10 oct 2021
    */

    //Temps------------------
    public int limiteTemps;
    public Text compteur;
    public GameObject helico;
    //Essence-----------------
    public Image barreEssence;
    public float carburant;
    public float consommation;
    private float reservoir;
    public static bool enPanne;
    public GameObject msgDanger;

    void Start()
    {
        reservoir = carburant;
    }

    void Update()
    {
        //Couleur Timer--------------------------------------------
        compteur.GetComponent<Text>().text = limiteTemps.ToString();

        if ((limiteTemps < 31) && (limiteTemps > 11))
        {
            compteur.GetComponent<Text>().color = Color.yellow;
        }
        else if (limiteTemps <= 10)
        {
            compteur.GetComponent<Text>().color = Color.red;
        }

        // Gestion de l'essence
        GestionEssence();
    }

    public void reduireTemps()
    {
        if ((limiteTemps > 0) && (deplacementHelico.finJeu==false))
        {
            if (deplacementHelico.drapeau == false) //Arrête le timer si le drapeau est atteint
            {
                Invoke("reduireTemps", 1f);
                limiteTemps -= 1;
            }
                
        } else
        {
            helico.GetComponent<deplacementHelico>().detruireHelico();
        }
    }
    void GestionEssence()
    {
        //pourcentage de carburant
        if(tourneObjet.conditionMoteur==true)
            carburant -= consommation;
        barreEssence.fillAmount = carburant / reservoir;

        //Limites de carburant
        if (carburant > reservoir)
            carburant = reservoir;
        if (carburant <= 0)
        {
            enPanne = true;
            carburant = 0;
        }
        else
        {
            enPanne = false;
        }

        // Moins de 30%
        if ((carburant / reservoir < 0.3)&&(deplacementHelico.finJeu==false))
        {
            msgDanger.SetActive(true);
        }
        else
        {
            msgDanger.SetActive(false);
        }

    }

}

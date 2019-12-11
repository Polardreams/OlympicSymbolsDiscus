using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class check_goal : MonoBehaviour
{
    private string[] goal_tag = new string[2];
    private int index_hit, index_goal;
    public Text txt;
    private bool goal_flag;
    static public bool ScreenResult;


    // Start is called before the first frame update
    void Start()
    {
        /**
         * Array ist eine Liste mit einer Anzahl an Symbolen
         * ist die Anzahl ungleich der Anzahl von Treffern
         * hat man verloren. Mit dieser Methode stelle ich
         * die reihenfolge durch die Anzahl der Treffer sicher
         * Möchte man dem Spieler eine mehrere Chancen geben,
         * dann muss der index_goal erhöht werden
         * **/

        goal_tag[0] = "blue";
        goal_tag[1] = "yellow";
        index_goal = 0;
        goal_flag = false;
        index_hit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (discus_physic.respawn_index<4) {//Diskus hat nicht 3x respant
            if (index_goal<=goal_tag.Length-1) {//der IST-Erreicht-Index ist kleiner als der SOLL-Index, 
                if (collision.tag == goal_tag[index_goal])//Kollision mit dem aktuellen GameObject (bei tag)
                {
                    txt.text = collision.tag;
                    index_goal++;//nur wenn das aktuelle Symbol getroffen wird, erhöht sich der Index und wählt das neue Ziel aus

                    StartCoroutine(destroySymbol(collision));

                    if (index_goal == goal_tag.Length)//IST-Erreicht-Index = SOLL-INdex, die anzahl der möglichen Ziele wurde getroffen
                    {
                        
                        goal_flag = true;
                        txt.text = "You Win";
                        ScreenResult = goal_flag;
                        SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
                    }
                }
            }
            index_hit++;
        } else//3x respawnt alle Versuche gescheitert, verloren
        {
            //goal_flag = false;
            goal_flag = false;
            txt.text = "You Loose";
                ScreenResult = goal_flag;
                SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
            discus_physic.respawn_index = 0;
        }
        
    }

    private IEnumerator destroySymbol (Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.GetComponent<Animator>().SetBool("ishit", true);
        collision.GetComponent<Collider2D>().enabled = false;
        
    }


}

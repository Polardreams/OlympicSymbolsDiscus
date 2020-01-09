using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class check_goal : MonoBehaviour
{
    //Scoresystem
    public GameObject[] hit_order = new GameObject[2];//Reihenfolge der Symbole zum Abschuss. Achtung die Tag's müssen gesetzt sein
    public int chances;
    private int index_goal;// Anzahl der bisherigen Treffer (pro Treffer 1x inkrementieren) Liste wird so weitergeführt
    private bool goal_flag;// Win or Lose flag für den ResultScreen


    //ScreenNavigation
    static public bool ScreenResult;


    // Start is called before the first frame update
    void Start()
    {
        index_goal = 0;

    }

    // Update is called once per frame
    void Update()
    {
        check_triggerEnter();
    }

    public void check_triggerEnter()
    {
        Collider2D collision = discus_collision.discusCollision;
        //if (discus_physic.respawn_index < hit_order.Length*2)

        if (discus_physic.respawn_index < chances)

        {//Diskus hat nicht 3x respawnt
            if (index_goal <= hit_order.Length - 1)
            {//der IST-Erreicht-Index ist kleiner als der SOLL-Index, 
                if (collision.tag == hit_order[index_goal].tag)//Kollision mit dem aktuellen GameObject (bei tag)
                {
                    index_goal++;//nur wenn das aktuelle Symbol getroffen wird, erhöht sich der Index und wählt das neue Ziel aus

                    StartCoroutine(destroySymbol(collision));

                    if (index_goal == hit_order.Length)//IST-Erreicht-Index = SOLL-INdex, die anzahl der möglichen Ziele wurde getroffen
                    {
                        goal_flag = true;
                        ScreenResult = goal_flag;
                        SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
                    }
                }
            }
        }
        else//3x respawnt alle Versuche gescheitert, verloren
        {
            goal_flag = false;
            ScreenResult = goal_flag;
            SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
            discus_physic.respawn_index = 0;
        }
    }

    private IEnumerator destroySymbol(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        collision.GetComponent<Animator>().SetBool("ishit", true);
        collision.GetComponent<Collider2D>().enabled = false;
    }

}

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
    private int max_hits;
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
        max_hits = 5;
        index_hit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (index_hit< max_hits) {
            if (index_goal<=goal_tag.Length-1) {
                if (collision.tag == goal_tag[index_goal])
                {
                    txt.text = collision.tag;
                    index_goal++;
                    if (index_goal == goal_tag.Length)
                    {
                        goal_flag = true;
                        txt.text = "You Win";
                        ScreenResult = goal_flag;
                        SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
                    }
                }
            }
            index_hit++;
        } else
        {
            //goal_flag = false;
            goal_flag = false;
            txt.text = "You Loose";
                ScreenResult = goal_flag;
                SceneManager.LoadScene("resultScreen", LoadSceneMode.Single);
        }
        
    }



}

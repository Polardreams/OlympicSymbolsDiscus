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
    private int index_goal;// Anzahl der bisherigen Treffer (pro Treffer 1x inkrementieren) Liste wird so weitergeführt
    private int score, points;
    public GameObject txt_score;
    public GameObject txt_points;
    public int scene_index;
    public static int index;



    //ScreenNavigation
    static public int medal_index; //gold = 2, silver = 1, bronxe = 0

    //Display
    private GameObject HUD_Rings;


    // Start is called before the first frame update
    void Start()
    {
        index_goal = 0;
        score = player_score.get_score();
        points = 0;
        HUD_Rings = GameObject.Find("HUD");
        index = scene_index;
    }

    // Update is called once per frame
    void Update()
    {
        check_Collision();
        check_freez();
    }

    private void check_Collision()
    {
        Collider2D collision = discus_collision.discusCollision;

        if (index_goal <= hit_order.Length - 1 && collision != null)
        {//der IST-Erreicht-Index ist kleiner als der SOLL-Index, 
            if (collision.tag == hit_order[index_goal].tag)//Kollision mit dem aktuellen GameObject (bei tag)
            {
                index_goal++;//nur wenn das aktuelle Symbol getroffen wird, erhöht sich der Index und wählt das neue Ziel aus
                StartCoroutine(destroySymbol(collision));
                score = count_score(1);
                display_points();
                display_score();
                HUD_ringsRemove(index_goal);
                if (index_goal == hit_order.Length)//Wenn alle Ziele getroffen sind ist die Stage zuende
                {
                    PlayerPrefs.SetInt("score", score);
                    scene_finish(index_goal, "resultScreen");
                }
            }
            else
            {
                score = count_score(0);//eventuelle festlegen, dass das Popup (points) nur bei Kontakt mit Ringen ausgelöst wird, nicht z.b. mit einer Wand
                display_points();
                display_score();
            }
        }
    }

    private void check_freez()
    {
        if (discus_physic.discusfreez)
        {
            PlayerPrefs.SetInt("score", score);
            scene_finish(index_goal, "resultScreen");
        }
    }

    private IEnumerator destroySymbol(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        collision.GetComponent<Animator>().SetBool("ishit", true);

        ParticleSystem p = collision.GetComponentInChildren<ParticleSystem>();
        p.enableEmission = true;
        p.Play();

        collision.GetComponent<Collider2D>().enabled = false;
    }

    private int count_score(int inc)
    {
        points = points + inc;
        return score = score + (points * inc);
    }

    private void display_points()
    {
        txt_points.GetComponent<Text>().text = "Points: " + points.ToString();
        //ev. Popup script
        //...
    }

    private void display_score()
    {
        txt_score.GetComponent<Text>().text = "Score: " + score.ToString();
    }

    private void HUD_ringsRemove(int i)
    {
        switch (i)
        {
            case 0: break;
            case 1: GameObject.Find("sr1").GetComponent<SpriteRenderer>().enabled = false; break;
            case 2: GameObject.Find("sr2").GetComponent<SpriteRenderer>().enabled = false; break;
            case 3: GameObject.Find("sr3").GetComponent<SpriteRenderer>().enabled = false; break;
            case 4: GameObject.Find("sr4").GetComponent<SpriteRenderer>().enabled = false; break;
            case 5: GameObject.Find("sr5").GetComponent<SpriteRenderer>().enabled = false; break;
        }
    }

    private void scene_finish(int hits, string nxt_stage)
    {
        switch (hits)
        {
            //Medalsystem
            case 5: medal_index = 2; break;
            case 4: medal_index = 1; break;
            case 3: medal_index = 1; break;
            case 2: medal_index = 0; break;
            case 1: medal_index = 0; break;
            case 0: medal_index = 0; break;
        }
        SceneManager.LoadScene(nxt_stage, LoadSceneMode.Single);
    }
}

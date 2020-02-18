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
    public GameObject txt_score;
    public int scene_index;

    public static int index;
    private int index_goal;// Anzahl der bisherigen Treffer (pro Treffer 1x inkrementieren) Liste wird so weitergeführt
    private int score, points;
    private Dictionary<int, int> critical_bounce = new Dictionary<int, int>();
    public static int discus_anz;
    private Vector2 startPos;
    private List<GameObject> instant = new List<GameObject>();
    public int bouncing;


    //ScreenNavigation
    static public int medal_index; //gold = 2, silver = 1, bronxe = 0

    //Display
    private GameObject HUD_Rings;

    public GameObject popup_points;
    public GameObject popup_TextMessage;


    // Start is called before the first frame update
    void Start()
    {
        index_goal = 0;
        discus_anz = 0;
        startPos = discus_physic.discus_startPosition;
        critical_bounce.Add(0, 0);
        critical_bounce.Add(1, 0);
        critical_bounce.Add(2, 0);
        critical_bounce.Add(3, 0);
        critical_bounce.Add(4, 0);

        score = player_score.get_score();
        points = 0;
        HUD_Rings = GameObject.Find("HUD");
        index = scene_index;
    }

    // Update is called once per frame
    void Update()
    {
        check_Collision();
        check_spanwDiscus();
        check_freez();
        if (GameObject.Find("player").transform.childCount == 0)
        {
            PlayerPrefs.SetInt("score", score);
            scene_finish(index_goal, "resultScreen");
        }
    }

    private void check_spanwDiscus()
    {
        if (discus_anz>0)
        {
            
            GameObject g = GameObject.Find("player").transform.GetChild(GameObject.Find("player").transform.GetChildCount()-1).gameObject;
            if (g.GetComponent<discus_physic>().throwStop==true)
            {
                int id = int.Parse(g.name.Replace("Discus", ""));
                discus_anz--;
                iniDiscus(g, id);
            }
        }
    }

    private void check_Collision()
    {
        //Collider2D colider = discus_collision.discusColider;
        Collision2D collision = discus_collision.discusCollision;
        if (index_goal <= hit_order.Length - 1 && collision != null)
        {//der IST-Erreicht-Index ist kleiner als der SOLL-Index, 
            if (collision.transform.tag == hit_order[index_goal].tag)//Kollision mit dem aktuellen GameObject (bei tag)
            {
                index_goal++;//nur wenn das aktuelle Symbol getroffen wird, erhöht sich der Index und wählt das neue Ziel aus
                discus_anz++;
                StartCoroutine(destroySymbol(collision));
                score = count_score(1);
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
                display_score();
                if (collision.transform.tag == "wall")
                {
                    int id = int.Parse(collision.otherCollider.name.Replace("Discus", ""));
                    switch (id)
                    {
                        case 0: critical_bounce[0]++; if (critical_bounce[0]>=0) { display_textMessage("critical" + critical_bounce[0].ToString()); }; break;
                        case 1: critical_bounce[1]++; if (critical_bounce[0]>=0) { display_textMessage("critical" + critical_bounce[1].ToString()); }; break;
                        case 2: critical_bounce[2]++; if (critical_bounce[0]>=0) { display_textMessage("critical" + critical_bounce[2].ToString()); }; break;
                        case 3: critical_bounce[3]++; if (critical_bounce[0]>=0) { display_textMessage("critical" + critical_bounce[3].ToString()); }; break;
                        case 4: critical_bounce[4]++; if (critical_bounce[0]>=0) { display_textMessage("critical" + critical_bounce[4].ToString()); }; break;
                    }
                    for (int n = 0; n < 5; n++)
                    {
                        if (critical_bounce[n] >= bouncing)
                        {
                            critical_bounce[n] = -1000;//hätte ihn auch löschen können
                            destroyDiscus(GameObject.Find(collision.otherCollider.name.ToString()), id);
                        }
                    }
                }
            }
        }
        discus_collision.discusCollision = null;
    }

    private void destroyDiscus(GameObject g, int id)
    {
        g.GetComponent<Animator>().enabled = false;
        g.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        g.GetComponent<discus_physic>().enabled = false;
        g.GetComponent<discus_collision>().enabled = false;
        g.GetComponent<player_score>().enabled = false;
        instant.Add(g);
        Invoke("instantdestroy", 5.0f);
    }


    private void iniDiscus (GameObject g, int id)
    {
            GameObject newDiscus = Instantiate(g, startPos, Quaternion.identity);
            newDiscus.transform.SetParent(GameObject.Find("player").transform);
            newDiscus.name = "Discus" + (id + 1).ToString();
            newDiscus.transform.localScale = new Vector2(1, 1);
        newDiscus.GetComponent<Rigidbody2D>().simulated = false;
            foreach (CircleCollider2D c in newDiscus.transform.GetComponents(typeof(CircleCollider2D)))
            {
                c.enabled = false;
            }
    }

    private void instantdestroy()
    {
        foreach (GameObject g in instant)
        {
            instant.Remove(g);
            GameObject.Destroy(g);
        }
    }

    private void check_freez ()
    {
        bool freezFlag = false;
        GameObject g = GameObject.Find("player");
        for (int n=0; n < g.transform.childCount;n++)
        {
            if (g.transform.GetChild(n).GetComponent<discus_physic>().discusfreez)
            {
                int id = int.Parse(g.transform.GetChild(n).name.Replace("Discus", ""));
                destroyDiscus(g.transform.GetChild(n).gameObject,id);
            }
        }

    }

    private IEnumerator destroySymbol(Collision2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        collision.transform.GetComponent<Animator>().SetBool("ishit", true);
        ParticleSystem p = collision.transform.GetComponentInChildren<ParticleSystem>();
        Vector2 v = collision.contacts[0].point;
        p.transform.position = v;
        display_points(v);
        p.enableEmission = true;
        p.Play();

        collision.transform.GetComponent<Collider2D>().enabled = false;
    }

    private int count_score(int inc)
    {
        points = points + inc;
        return score = score + (points * inc);
    }

    private void display_textMessage (string txt)
    {
        popup_TextMessage.GetComponent<TMPro.TextMeshPro>().text = txt;
        popup_TextMessage.GetComponent<Animator>().SetBool("pop", true);
        popup_TextMessage.GetComponent<MeshRenderer>().enabled = true;
    }

    private void display_points(Vector2 v)
    {
        popup_points.GetComponent<Animator>().SetBool("pop", true);
        popup_points.GetComponent<MeshRenderer>().enabled = true;
        popup_points.transform.position = v;
        
        if (points>1)
        {
            popup_points.GetComponent<TMPro.TextMeshPro>().text = points.ToString() + " Points";
        } else
        {
            popup_points.GetComponent<TMPro.TextMeshPro>().text = points.ToString() + " Point";
        }
    }



    private void display_score()
    {
        txt_score.GetComponent<Text>().text = "Score: " + score.ToString()+'\n'+"Count Discus "+discus_anz.ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discus_collision : MonoBehaviour
{
    public static Collision2D discusCollision;
    public AudioSource hit1, hit2, hit3, hit4, hit5, wall_top, wall_down, next_disc;
    public static AudioSource next_disc_static;


    // Start is called before the first frame update
    void Start()
    {
        next_disc_static = next_disc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static void play_next_disc()
    {
        next_disc_static.PlayOneShot(next_disc_static.clip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        discusCollision = collision;

        /*
         *Tag System
         * alle GameObjects haben bestimmten Tag, dieser 
         * kann einer bestimmten AudioSource zugeordnet werden
         */

        switch (collision.transform.tag)
        {
            case "wall_top": wall_top.PlayOneShot(wall_top.clip); break;
            case "wall_down": wall_down.PlayOneShot(wall_down.clip); break;
            case "green": hit1.PlayOneShot(hit1.clip); break;
            case "blue": hit2.PlayOneShot(hit2.clip); break;
            case "yellow": hit3.PlayOneShot(hit3.clip); break;
            case "black": hit4.PlayOneShot(hit4.clip); break;
            case "red": hit5.PlayOneShot(hit5.clip); break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discus_collision : MonoBehaviour
{
    public static Collider2D discusCollision;
    public AudioSource hit, bounce;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        discusCollision = collision;

        /*
         *Tag System
         * alle GameObjects haben bestimmten Tag, dieser 
         * kann einer bestimmten AudioSource zugeordnet werden
         */

        switch (collision.tag)
        {
            case "ground": bounce.PlayOneShot(bounce.clip); break;
            case "green": hit.PlayOneShot(hit.clip); break;
            case "blue": hit.PlayOneShot(hit.clip); break;
            case "yellow": hit.PlayOneShot(hit.clip); break;
            case "black": hit.PlayOneShot(hit.clip); break;
            case "red": hit.PlayOneShot(hit.clip); break;
        }
        
    }

}

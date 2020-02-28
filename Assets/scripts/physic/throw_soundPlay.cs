using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_soundPlay : MonoBehaviour
{
    public AudioSource disc_throw;
    // Start is called before the first frame update
    public void play_disc_throw()
    {
        if (disc_throw!=null)
        {
            disc_throw.PlayOneShot(disc_throw.clip);
        } 
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_properties : MonoBehaviour
{
    // Start is called before the first frame update
    public void Aset_pop_points_false()
    {
        gameObject.GetComponent<Animator>().SetBool("pop", false);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ini : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int unlockStages = PlayerPrefs.GetInt("unlockStages");
        for (int n = 2; n<unlockStages+1; n++ )
        {
            GameObject.Find("btn_level_" + n.ToString()).GetComponent<Button>().interactable = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

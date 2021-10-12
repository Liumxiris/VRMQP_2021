using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Color initialColor;
    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void HoverOver() {
        //GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void HoverEnd()
    {
        //GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        GetComponent<Renderer>().material.color = initialColor;
    }
}

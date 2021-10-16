using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    XRDirectInteractor LeftController;
    [SerializeField]
    XRDirectInteractor RightController;

    bool isGrabbing = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(LeftController.selectTarget.gameObject);
    }

    public void Select() {
        isGrabbing = true;
        //Debug.Log(gameObject.name + " Selecting");
    }

    public void Deselect()
    {
        isGrabbing = false;
    }
}

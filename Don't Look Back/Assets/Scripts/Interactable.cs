using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    int Type;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip pickedUpSound;

    //Color initialColor;
    bool m_Grabbed = false;
    List<GameObject> collideObjects = new List<GameObject>();
    GameObject Player;

    Color BoxColor = Color.white;
    Vector3 InitialPos;
    Quaternion InitialRot;


    // Start is called before the first frame update
    void Start()
    {
        //initialColor = GetComponent<Renderer>().material.color;
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        InitialPos = transform.position;
        InitialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (!m_Grabbed && collision.gameObject.tag == "ClassifyBox") {
            // Add sanity
            // Play sound
            //Destroy(this);
        }
    }
    */


    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.tag == "ClassifyBox")
        {
            collideObjects.Add(other.gameObject);
        } else if (other.gameObject.tag == "Bound") {
            transform.position = InitialPos;
            transform.rotation = InitialRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "ClassifyBox")
        {
            other.gameObject.GetComponent<Renderer>().material.color = BoxColor;
            //Debug.Log("Leaving reset color");
        }
    }

    void LateUpdate()
    {
        GameObject closest = null;
        foreach (GameObject go in collideObjects)
        {
            if (closest == null)
            {
                closest = go;
            }
            else if ((go.transform.position - transform.position).magnitude < (closest.transform.position - transform.position).magnitude)
            {
                closest = go;
            }
        }
        if (closest == null) return;

        // Highlight cloest
        closest.GetComponent<Renderer>().material.color = Color.yellow;

        foreach (GameObject go in collideObjects)
        {
            if (go != closest)
            {
                go.GetComponent<Renderer>().material.color = BoxColor;
            }

        }
        
        collideObjects = new List<GameObject>();

        if (m_Grabbed) return;


        if (Type == closest.gameObject.GetComponent<ClassifyBox>().Type)
        {
            // Add sanity
            Player.GetComponent<PlayerLogic>().updateSanity(6);
            // Play sound
            Debug.Log("Correct Classification!");
            // Put into box
            closest.gameObject.GetComponent<ClassifyBox>().StashFile(this.gameObject);
        }
        else
        {
            // Penalty
            // Play sound
            Debug.Log("Wrong Classification!");
            // Destroy Self
            Destroy(gameObject);
        }

        // Reset Color
        closest.GetComponent<Renderer>().material.color = BoxColor;
    }



    public void Select()
    {
        m_Grabbed = true;
        audioSource.PlayOneShot(pickedUpSound);
    }

    public void Deselect()
    {
        m_Grabbed = false;
    }

    /*
    public void HoverOver() {
        //GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void HoverEnd()
    {
        //GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        GetComponent<Renderer>().material.color = initialColor;
    }
    */
}

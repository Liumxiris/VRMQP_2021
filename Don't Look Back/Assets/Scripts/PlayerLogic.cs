using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]
    Text SanityText;

    float Sanity;
    float decreaseSpeed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Sanity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        changeSanity(-Time.deltaTime * decreaseSpeed);
    }

    public void changeSanity(float value) {
        Sanity += value;

        if (Sanity > 100) {
            Sanity = 100;
        }else if (Sanity <= 0) {
            Sanity = 0;
            //Game Over
        }

        SanityText.text = "Sanity : " + (Mathf.Round(Sanity * 10f) / 10f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]
    Text SanityText;

    [SerializeField]
    Camera PlayerCam;

    [SerializeField]
    GameObject Monster;

    [SerializeField]
    LayerMask MonsterLayer;

    [SerializeField]
    float MonsterRemoveTime = 3f;

    float monsterCounter = 0f;
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
        if (isTurnBack())
        {
            //Debug.Log("Turing Back!");
            updateSanity(-Time.deltaTime * decreaseSpeed * 2);
        }
        else
            updateSanity(-Time.deltaTime * decreaseSpeed);

        //Debug.DrawRay(PlayerCam.transform.position, PlayerCam.transform.forward * 10f, Color.red);
        if (isLookingAtMonster()) {
            monsterCounter += Time.deltaTime;
        }
        else if(monsterCounter > 0)
        {
            monsterCounter -= Time.deltaTime / 100f;
        }

        if(monsterCounter >= MonsterRemoveTime)
        {
            Monster.GetComponent<MonsterLogic>().returnToStart();
            monsterCounter = 0;
        }
    }

    public void updateSanity(float value) {
        Sanity += value;

        if (Sanity > 100) {
            Sanity = 100;
        } else if (Sanity <= 0) {
            Sanity = 0;
            //Game Over
        }

        SanityText.text = "Sanity : " + (Mathf.Round(Sanity * 10f) / 10f);
    }

    bool isTurnBack() {
        float rotationValue = PlayerCam.transform.rotation.eulerAngles.y;
        if (rotationValue > 80 && rotationValue < 280) 
            return true;
        else
            return false;
    }

    bool isLookingAtMonster() {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, 20f, MonsterLayer))
        {
            if (hit.collider.gameObject.tag == "Monster")
                return true;
            else
                return false;
        }
        else return false;
    }
}

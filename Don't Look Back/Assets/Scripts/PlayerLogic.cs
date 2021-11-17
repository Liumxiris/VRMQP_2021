using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    int TargetFileClassified;

    [SerializeField]
    Text TargetFile;

    [SerializeField]
    Text FileCompletion;

    [SerializeField]
    GameObject Lamp;

    float monsterCounter = 0f;
    float Sanity;
    float decreaseSpeed = 1.0f;

    int fileCompleted = 0;

    bool reached80 = false;
    bool reached60 = false;
    bool reached40 = false;

    public AudioSource heartbeat;

    // Start is called before the first frame update
    void Start()
    {
        TargetFile.text = "Target File Completion : " + TargetFileClassified;
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
            updateSanity(-Time.deltaTime * decreaseSpeed * 4);
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

        if (Sanity < 40f)
        {
            if (!heartbeat.isPlaying)
            {
                heartbeat.Play();
            }
        }
        else 
        {
            if (heartbeat.isPlaying)
            {
                heartbeat.Stop();
            }
        }
    }

    public void updateSanity(float value) {
        Sanity += value;

        if (Sanity > 100) {
            Sanity = 100;
        }
        if (Sanity < 80f && !reached80)
        {
            Lamp.GetComponent<BlinkingLight>().ReduceLife();
            Lamp.GetComponent<BlinkingLight>().BlinkForTime(3);
            reached80 = true;
        }
        if (Sanity < 60f && !reached60)
        {
            Lamp.GetComponent<BlinkingLight>().ReduceLife();
            Lamp.GetComponent<BlinkingLight>().BlinkForTime(5);
            reached60 = true;
        }
        if (Sanity < 40f && !reached40)
        {
            Lamp.GetComponent<BlinkingLight>().ReduceLife();
            reached40 = true;
        }
        if (Sanity <= 0) {
            Sanity = 0;
            //Game Over
            SceneManager.LoadScene("Lose");
        }

        SanityText.text = "Sanity : " + (Mathf.Round(Sanity * 10f) / 10f);
    }

    public void updateFileCompletion() {
        fileCompleted++;
        FileCompletion.text = "File Completed : " + fileCompleted;
        if (fileCompleted >= TargetFileClassified) {
            // Game Win
            SceneManager.LoadScene("Win");
        }
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

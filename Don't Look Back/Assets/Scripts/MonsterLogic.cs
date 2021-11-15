using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Path1 = new List<GameObject>();

    [SerializeField]
    List<GameObject> Path2 = new List<GameObject>();

    [SerializeField]
    List<GameObject> Path3 = new List<GameObject>();

    List<List<GameObject>> Path = new List<List<GameObject>>();

    [SerializeField]
    GameObject Player;

    [SerializeField]
    float Speed = 1f;

    [SerializeField]
    float RotationSpeed = 5f;

    [SerializeField]
    float CoolDown = 3f;

    [SerializeField]
    Renderer MonsterRenderer;

    [SerializeField]
    float InvisCD;

    [SerializeField]
    float InvisTime;

    bool InvisCDing;
    float InvisCDCount;
    float InvisTimeCount;

    bool ChagingAlpha;
    float CurrentAlpha;
    float TargetAlpha;

    float CoolDownCount = 0;
    bool CoolDowning = false;

    List<GameObject> CurrentPath = new List<GameObject>();
    GameObject NextWP;
    int Step;

    public AudioSource breath;

    [SerializeField]
    AudioClip monsterVanish;

    [SerializeField]
    AudioClip monsterInvis;

    [SerializeField]
    AudioClip open_close_door;

    [SerializeField]
    AudioClip collideBox;

    [SerializeField]
    AudioClip light_flickers;

    [SerializeField]
    AudioClip collideBookshelf;

    // Start is called before the first frame update
    void Start()
    {
        Path.Add(Path1);
        Path.Add(Path2);
        Path.Add(Path3);
        StartRandomPath();
        //ChangeAlpha(0);
    }

    void StartRandomPath()
    {
        CurrentPath = Path[Random.Range(0, Path.Count)];
        transform.position = CurrentPath[0].transform.position;
        NextWP = CurrentPath[1];
        Step = 1;
        AudioSource.PlayClipAtPoint(open_close_door, CurrentPath[0].transform.position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (CoolDowning)
        {
            if (CoolDownCount > CoolDown)
            {
                StartRandomPath();
                CoolDowning = false;
            }
            else
            {
                CoolDownCount += Time.deltaTime;
            }
        }
        else
        {
            followPath();

            if (InvisCDing)
            {
                InvisTimeCount += Time.deltaTime;
                if(InvisTimeCount >= InvisTime)
                {
                    ChangeAlpha(1);
                    InvisTimeCount = 0;
                    InvisCDing = false;
                }
            }
            else
            {
                InvisCDCount += Time.deltaTime;
                if(InvisCDCount >= InvisCD)
                {
                    AudioSource.PlayClipAtPoint(monsterInvis, transform.position, 1);
                    ChangeAlpha(0);
                    InvisCDCount = 0;
                    InvisCDing = true;
                }
            }

            if (ChagingAlpha)
            {
                if (Mathf.Abs(TargetAlpha - CurrentAlpha) <= 0.1)
                {
                    ChangingAlpha(TargetAlpha);
                    ChagingAlpha = false;
                }
                else
                {
                    ChangingAlpha(Mathf.Lerp(CurrentAlpha, TargetAlpha, Time.deltaTime));
                }
            }

            if (!breath.isPlaying)
            {
                breath.Play();
            }
        }
    }

    void followPath() {
        // If reached next WP
        if ((transform.position - CurrentPath[Step].transform.position).magnitude < 0.1f) {
            if (Step < CurrentPath.Count - 1)
            {
                Step++;
                NextWP = CurrentPath[Step];
            }
            else {
                // Get to player
                Player.GetComponent<PlayerLogic>().updateSanity(-50f);
                returnToStart();
            }
        }

        // Turn to correct angle
        turnToNextWP();

        // Move
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    void turnToNextWP() {
        Vector3 vectorToTarget = NextWP.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.z, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.down);
        //transform.rotation = q;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
    }

    public void returnToStart()
    {
        AudioSource.PlayClipAtPoint(monsterVanish, transform.position, 1);
        CoolDownCount = 0;
        CoolDowning = true;
        transform.position = new Vector3(100,0,0);
    }

    void ChangeAlpha(float alpha)
    {
        TargetAlpha = alpha;
        CurrentAlpha = MonsterRenderer.material.color.a;
        ChagingAlpha = true;
    }

    void ChangingAlpha(float alpha)
    {
        Color monsterColor = MonsterRenderer.material.color;
        CurrentAlpha = monsterColor.a;
        monsterColor.a = alpha;
        MonsterRenderer.material.color = monsterColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Trigger")
        {
            if (other.gameObject.name == "TriggerPoint_Box1") // collide with Box1
            {
                AudioSource.PlayClipAtPoint(collideBox, other.transform.position, 1);
            }
            else if (other.gameObject.name == "TriggerPoint_Box2") // collide with Box2
            {
                AudioSource.PlayClipAtPoint(collideBox, other.transform.position, 1);
            }
            else if (other.gameObject.name == "TriggerPoint_TallLamp2") // collide with tall lamp on behind
            {
                AudioSource.PlayClipAtPoint(light_flickers, other.transform.position, 1);
            }
            else if (other.gameObject.name == "TriggerPoint_Bookshelf_Left" || other.gameObject.name == "TriggerPoint_Bookshelf_Right") // collide with bookshelfs
            {
                AudioSource.PlayClipAtPoint(collideBookshelf, other.transform.position, 1);
            }
            else if (other.gameObject.name == "TriggerPoint_Tablelamp_behind") // collide with table lamp on behind
            {
                AudioSource.PlayClipAtPoint(light_flickers, other.transform.position, 1);
            }
        }
    }
}

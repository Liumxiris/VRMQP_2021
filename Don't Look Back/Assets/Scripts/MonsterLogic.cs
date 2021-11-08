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

    float CoolDownCount = 0;
    bool CoolDowning = false;

    List<GameObject> CurrentPath = new List<GameObject>();

    GameObject NextWP;

    int Step;

    // Start is called before the first frame update
    void Start()
    {
        Path.Add(Path1);
        Path.Add(Path2);
        Path.Add(Path3);
        StartRandomPath();
    }

    void StartRandomPath()
    {
        CurrentPath = Path[Random.Range(0, Path.Count)];
        transform.position = CurrentPath[0].transform.position;
        NextWP = CurrentPath[1];
        Step = 1;
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
        CoolDownCount = 0;
        CoolDowning = true;
        transform.position = new Vector3(100,0,0);
    }
}

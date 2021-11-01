using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Path1 = new List<GameObject>();


    [SerializeField]
    float Speed = 1f;

    [SerializeField]
    float RotationSpeed = 5f;


    List<GameObject> CurrentPath = new List<GameObject>();

    GameObject NextWP;

    int Step;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPath = Path1;
        NextWP = CurrentPath[0];
        Step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        followPath();
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
                // Looping for now
                Step = 0;
                NextWP = CurrentPath[Step];
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
}

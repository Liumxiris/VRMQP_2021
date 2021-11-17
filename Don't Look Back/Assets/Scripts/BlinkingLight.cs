using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField]
    GameObject Light;

    [SerializeField]
    float DarkInterval;

    [SerializeField]
    float BlinkInterval;

    [SerializeField]
    float BlinkIntervalOffset;

    [SerializeField]
    int Life;

    [SerializeField]
    bool Blinking = false;

    float CurrentInterval;
    float BlinkIntervalCount = 0;
    float DarkIntervalCount = 0;
    float BlinkingTimeCount = 0;

    float BlinkingTime = 0;

    bool Dark = false;
    bool LightOff = false;
    // Start is called before the first frame update
    void Start()
    {
        Light.SetActive(!Dark);
        CurrentInterval = BlinkInterval + Random.Range(-BlinkIntervalOffset, BlinkIntervalOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (LightOff) return;
        if (Blinking)
        {
            if (BlinkingTime != 0) {
                BlinkingTimeCount += Time.deltaTime;
                if (BlinkingTimeCount >= BlinkingTime) {
                    Blinking = false;
                    BlinkingTimeCount = 0;
                    BlinkingTime = 0;
                }
            }
            if (!Dark)
            {
                BlinkIntervalCount += Time.deltaTime;
                if (BlinkIntervalCount >= CurrentInterval)
                {
                    Dark = true;
                    Light.SetActive(!Dark);
                    BlinkIntervalCount = 0;
                    CurrentInterval = BlinkInterval + Random.Range(-BlinkIntervalOffset, BlinkIntervalOffset);
                }
            }
            else
            {
                DarkIntervalCount += Time.deltaTime;
                if (DarkIntervalCount >= DarkInterval)
                {
                    Dark = false;
                    Light.SetActive(!Dark);
                    DarkIntervalCount = 0;
                }
            }
        }
    }

    public void ReduceLife() {
        Life--;
        if (Life <= 0) {
            Light.SetActive(false);
            LightOff = true;
        }
    }

    public void BlinkForTime(float time) {
        Blinking = true;
        BlinkingTime = time;
        BlinkingTimeCount = 0;
    }

}

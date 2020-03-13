using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float turn_time;

    private float timeLeft;

    public Text text;

    public bool isWhiteTurn;

    private void Start()
    {
        timeLeft = turn_time;
        isWhiteTurn = true;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "00:0" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            text.text = "Next!";
            if (isWhiteTurn == true)
            {
                isWhiteTurn = false;
            }
            else
            {
                isWhiteTurn = true;
            }
            StartCoroutine(restart());
        }
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(1f);
        timeLeft = turn_time;
        StopCoroutine(restart());  
    }

}


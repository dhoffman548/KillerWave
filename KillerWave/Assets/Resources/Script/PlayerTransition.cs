﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    Vector3 transitionToEnd = new Vector3(-100, 0, 0);
    Vector3 transitionToCompleteGame = new Vector3(7000, 0, 0);
    Vector3 readyPos = new Vector3(900, 0, 0);
    Vector3 startPos;
    float distCovered;
    float journeyLength;
    bool levelStarted = true;
    bool speedOff = false;
    bool gameCompleted = false;
 

    public bool levelEnds
    {
        get {return levelEnds;}
        set {levelEnds = value;}
    }
    public bool GameCompleted
    {
        get { return gameCompleted; }
        set { gameCompleted = value; }
    }

    private void Start()
    {
        this.transform.localPosition = Vector3.zero;
        startPos = transform.position;
        Distance();
    }
    void Distance()
    {
        journeyLength = Vector3.Distance(startPos, readyPos);
    }
    private void Update()
    {
        if (levelStarted)
        {
            StartCoroutine(PlayerMovement(transitionToEnd, 10));
        }
        if (levelEnds)
        {
            GetComponent<Player>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            Distance();
            StartCoroutine(PlayerMovement(transitionToEnd, 200));
        }
        if (gameCompleted)
        {
            GetComponent<Player>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(PlayerMovement(transitionToCompleteGame, 200));
        }
        if (speedOff)
        {
            Invoke("SpeedOff", 1f);
        }
    }
    IEnumerator PlayerMovement(Vector3 point, float transitionSpeed)
    {
        if (Mathf.Round(transform.localPosition.x) >= readyPos.x -5 && Mathf.Round(transform.localPosition.x) <= readyPos.x +5 && Mathf.Round(transform.localPosition.y) >= -5f && Mathf.Round(transform.localPosition.y) <= +5f)
        {
            if (levelEnds)
            {
                levelEnds = false;
                speedOff = true;
            }
            if (levelStarted)
            {
                levelStarted = false;
                distCovered = 0;
                GetComponent<Player>().enabled = true;
            }
            else
            {
                distCovered += Time.deltaTime * transitionSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, point, fractionOfJourney);
            }
            yield return null;
        }
    }
    void SpeedOff()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 800);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    static bool isOn = false;
    static event Action GameEnded;
    [SerializeField] GameObject enemies;
    [SerializeField] GameObject allies;
    [SerializeField] GameObject stationSprite;
    [SerializeField] GameObject stationExplosion;
    [SerializeField] Text scoreUI;
    static float score = 0f;
    float scoreDelay = 0.1f;
    float timer;
    int cameraZoomStart = 2;
    int cameraZoomGameplay = 17;

    public static bool IsOn
    {
        get { return isOn; }
    }

    public float Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            scoreUI.text = "" + score;
        }
    }

    

    // Use this for initialization
    void Start ()
    {
        Camera.main.orthographicSize = cameraZoomStart;
        GameEnded += HideStation;
        GameEnded += PlayStationExplosion;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!Game.isOn)
        {
            if (Input.touchCount > 0)
            {
                StartGame();
                Game.isOn = true;
            }

            return;
        }

        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = scoreDelay;
            Score++;
        }
    }

    void StartGame()
    {
        int numberOfEnemies = enemies.transform.childCount;
        for (int i = numberOfEnemies - 1; i >= 0; i--)
        {
            Destroy(enemies.transform.GetChild(i).gameObject);
        }

        int numberOfAllies = allies.transform.childCount;
        for (int i = numberOfAllies - 1; i >= 0; i--)
        {
            Destroy(allies.transform.GetChild(i).gameObject);
        }

        StartCoroutine(ZoomOutCamera());
        ShowStation();
        Score = 0;
        //TODO energy loading
        EnergyBarBehavior.Energy = 100;
    }

    public static void EndGame()
    {
        Game.isOn = false;
        GameEnded.Invoke();
    }

    void PlayStationExplosion()
    {
        //TODO explosion rotation
        stationExplosion.SetActive(true);
        PlayableDirector explosion = stationExplosion.GetComponentInChildren<PlayableDirector>();
        explosion.Play();
        explosion.stopped += StationExploded;
    }

    private void StationExploded(PlayableDirector pd)
    {
        stationExplosion.SetActive(false);
    }

    void ShowStation()
    {
        stationSprite.SetActive(true);
    }

    void HideStation()
    {
        stationSprite.SetActive(false);
    }

    IEnumerator ZoomOutCamera()
    {
        while (Camera.main.orthographicSize < cameraZoomGameplay)
        {
            Camera.main.orthographicSize++;
            yield return null;
            yield return null;
        }
    }
}

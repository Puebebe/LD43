using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    static bool isOn = false;
    static bool isEnded = false;
    static event Action GameEnded;
    [SerializeField] GameObject enemies;
    [SerializeField] GameObject allies;
    [SerializeField] GameObject stationSprite;
    [SerializeField] GameObject stationExplosion;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject bestScoreUI;
    [SerializeField] GameObject energyBarUI;
    int score = 0;
    int bestScore;
    float scoreDelay = 0.1f;
    float timer;
    int cameraZoomStart = 2;
    int cameraZoomGameplay = 17;
    Coroutine cameraZoom;

    public static bool IsOn
    {
        get { return isOn; }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            scoreUI.GetComponent<Text>().text = "" + score;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Camera.main.orthographicSize = cameraZoomStart;
        GameEnded += PlayStationExplosion;
        GameEnded += UpdateBestScore;
        GameEnded += ShowEndScreen;
        SetBestScore();
        ShowStartScreen();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!Game.isOn)
        {
            if (Game.isEnded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !stationExplosion.activeSelf)
            {
                int numberOfEnemies = enemies.transform.childCount;
                for (int i = numberOfEnemies - 1; i >= 0; i--)
                {
                    Destroy(enemies.transform.GetChild(i).gameObject);
                }

                ShowStartScreen();
                cameraZoom = StartCoroutine(ZoomInCamera());
                Game.isEnded = false;
            }
            else if (!Game.isEnded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
        ShowGameScreen();

        int numberOfAllies = allies.transform.childCount;
        for (int i = numberOfAllies - 1; i >= 0; i--)
        {
            Destroy(allies.transform.GetChild(i).gameObject);
        }

        if (cameraZoom != null)
            StopCoroutine(cameraZoom);
        StartCoroutine(ZoomOutCamera());
        Score = 0;
        StartCoroutine(EnergyBarBehavior.EnergyLoading());
        EnergyBarBehavior.isRegenerationOn = true;
    }

    public static void EndGame()
    {
        Game.isOn = false;
        Game.isEnded = true;
        GameEnded.Invoke();
    }

    void PlayStationExplosion()
    {
        stationExplosion.transform.rotation = stationSprite.transform.rotation;
        stationExplosion.SetActive(true);
        PlayableDirector explosion = stationExplosion.GetComponentInChildren<PlayableDirector>();
        explosion.Play();
        explosion.stopped += StationExploded;
        Handheld.Vibrate();
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

    void SetBestScore()
    {
        if (PlayerPrefs.HasKey("bestgameever"))
        {
            bestScore = PlayerPrefs.GetInt("bestgameever");
        }
        else
            bestScore = 0;

        bestScoreUI.GetComponent<Text>().text = "BEST: " + bestScore;
    }

    void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestgameever", score);
            PlayerPrefs.Save();
            bestScoreUI.GetComponent<Text>().text = "BEST: " + bestScore;
        }
    }

    void ShowStartScreen()
    {
        ShowStation();
        scoreUI.SetActive(false);
        bestScoreUI.SetActive(true);
        energyBarUI.SetActive(false);
    }

    void ShowGameScreen()
    {
        ShowStation();
        scoreUI.SetActive(true);
        scoreUI.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
        bestScoreUI.SetActive(false);
        energyBarUI.SetActive(true);
    }

    void ShowEndScreen()
    {
        HideStation();
        scoreUI.SetActive(true);
        scoreUI.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
        bestScoreUI.SetActive(true);
        energyBarUI.SetActive(false);
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

    IEnumerator ZoomInCamera()
    {
        while (Camera.main.orthographicSize > cameraZoomStart)
        {
            Camera.main.orthographicSize--;
            yield return null;
            yield return null;
        }
    }
}

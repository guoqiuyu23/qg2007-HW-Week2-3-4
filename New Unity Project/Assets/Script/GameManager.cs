using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public TextMesh text;
    public TextMesh text2;
    public int score = 0;
    public static GameManager instance;
    public int level = 1;
    private float gameTimer = 0;
    private float shortestTime = 9999999;
    private float gameEndTime = 0;
    private const string FILE_SHORTEST_TIMES = "/Logs/shortestTime.txt";
    private String FILE_PATH_SHORTEST_TIMES;
    private const string FILE_STIMES = "/Logs/stimes.txt";
    private String FILE_PATH_STIMES;
    private List<float> stimes;

    public float GameEndTime
    {
        get
        {
            return gameEndTime;
        }
        set
        {
            gameEndTime = value;
            Debug.Log(message:"GamEndTimeChanged!!!");
            if (gameEndTime < ShortestTime)
            {
                ShortestTime = gameEndTime;
            }
        }

    }

    public float ShortestTime
    {
        get
        {
            if (shortestTime > 1000) 
            {
                if (File.Exists(FILE_PATH_SHORTEST_TIMES))
                {
                    shortestTime = float.Parse(File.ReadAllText(FILE_PATH_SHORTEST_TIMES));
                }
            }
            else
            {
                shortestTime = 999;
            }
            
            return shortestTime;
        }
        set
        {
            shortestTime = value; 
            Debug.Log(message:"shortestTime SET!!!");
            File.WriteAllText(FILE_PATH_SHORTEST_TIMES, shortestTime + "");
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        text.text = "timer: " + gameTimer;  
        gameTimer = 0;
        FILE_PATH_SHORTEST_TIMES = Application.dataPath + FILE_SHORTEST_TIMES;
        FILE_PATH_STIMES = Application.dataPath + FILE_STIMES;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (score<4)
        {   
            text.text = "timer: " + gameTimer; 
            gameTimer += Time.deltaTime * 1;
        }
        
        if (score ==2 && level==1)
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
            level++;
        }

        if (score == 4)
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
            Debug.Log(message: "level2 done");
            GameEndTime = gameTimer;
            Debug.Log(message: "gameEndTime set");
            score = 5;
            gameTimer = 0;
            if (gameEndTime < shortestTime)
            {
                shortestTime = gameEndTime;
            }

            // text.text = "timer: " + gameEndTime + "\nshortest time: " + shortestTime;
            ////////////////////////
            UpdateStimes();
            string stimesString = "best records:\r\n";
            for (var i = 0; i < stimes.Count; i++)
            {
                stimesString += stimes[i] + "\n";
            }
            text.text = "timer: " + gameEndTime + "\nshortest time: " + shortestTime +"\r\n\r\n" + stimesString;
            
        }

        
    }


    void UpdateStimes()
    {
        if (stimes == null)
        {
            stimes = new List<float>();
            String fileConents2 = File.ReadAllText(FILE_PATH_STIMES);
            string[] fileStimes = fileConents2.Split(',');
            for (var i = 0; i < fileStimes.Length-1; i++)
            {
                stimes.Add(float.Parse(fileStimes[i]));
            }
        }

        for (var i = 0; i < stimes.Count; i++)
        {
            if (gameEndTime < stimes[i])
            {
                stimes.Insert(index: i, item: gameEndTime);
                break;
            }
        }
        string saveStimesString = "";
        for (var i = 0; i < stimes.Count; i++)
        {
            saveStimesString += stimes[i] + ",";
        }
        File.WriteAllText(FILE_PATH_STIMES, saveStimesString + "");
    }
}

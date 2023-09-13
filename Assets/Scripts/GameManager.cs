using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public Score score;
    public Spawner spawner;
    public Blade blade;

    public int point = -1;
    private float waitTime = 2;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    IEnumerator Start()
    {
        while (spawner == null && blade == null)
        {
            yield return null;
        }
        spawner.enabled = false;
        blade.enabled = false;
    }
    public void ScoreUpdate()
    {
        score.UpdateScore();
    }
    public void GameStart()
    {
        ClearScene();
        StartCoroutine(starting());
        point = -1;
        score.UpdateScore();
    }
    public void GameOver()
    {
        spawner.enabled = false;
        blade.enabled = false;
        score.gameObject.SetActive(false);
        ClearScene();
    }
    IEnumerator starting()
    {
        yield return new WaitForSeconds(waitTime);
        spawner.enabled = true;
        blade.enabled = true;
        score.gameObject.SetActive(true);
    }
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit f in fruits)
        {
            Destroy(f.gameObject);
        }
        Bomb[] bombs = FindObjectsOfType<Bomb>();   
        foreach (Bomb b in bombs)
        {
            Destroy(b.gameObject);  
        }
    }
}

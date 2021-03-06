﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatewayManager : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Vector3 previousPlayerPosition;
    private string previousSceneName;
    private bool spawnPrepared;
    private bool battleScene;
    string battleSceneName;
    public static GatewayManager Instance { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(spawnPrepared)
        {
            MovePosition();
        }
    }
   
    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        spawnPrepared = true;
        this.spawnPosition = spawnPosition;
    }

    private void MovePosition()
    {
        if (battleScene)
        {
            FindObjectOfType<PlayerMovement>().DisablePlayer();
        }
            FindObjectOfType<PlayerMovement>().TeleportTo(spawnPosition);
            spawnPrepared = false;
    }

    public void IsBattleScene(bool battle)
    {
        battleScene = battle;
    }

    //Tallenetaan edellisen scenen tiedot.
    public void PreviousSceneData(Vector3 prevPlayer, string prevScene)
    {
        previousPlayerPosition = prevPlayer;
        previousSceneName = prevScene;
    }

    public void MoveToPrevScene()
    {
        //SetSpawnPosition(previousPlayerPosition);
        //SceneManager.LoadScene(previousSceneName);
        //Scene sceneName = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(battleSceneName);
        FindObjectOfType<DisableEverything>().ToggleOn(true);
    }

    public void UpdateBattleSceneName(string bName)
    {
        battleSceneName = bName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLauncher : MonoBehaviour
{
    public List<Character> Players { get; set; }
    public List<Character> Enemies { get; set; }
    public string battleSceneName = "SampleScene";
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PrepareBattle(List<Character> enemies, List<Character> players)
    {
        Players = players;
        Enemies = enemies;
        FindObjectOfType<DisableEverything>().ToggleOn(false);
        GatewayManager.Instance.UpdateBattleSceneName(battleSceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(battleSceneName,UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void Launch()
    {
        BattleController.Instance.StartBattle(Players, Enemies);
    }
}

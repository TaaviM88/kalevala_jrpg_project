using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway : MonoBehaviour
{
    [SerializeField]
    private string SceneName;

    [SerializeField]
    private Vector3 spawnLocation;

    [SerializeField]
    private bool Enemy;

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GatewayManager.Instance.SetSpawnPosition(spawnLocation);
            SceneManager.LoadScene(SceneName);
        }
    }
    */

    public void ChangeScene()
    {
        if (!Enemy) {
            MoveToScene();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveToScene();
        }
    }

    private void MoveToScene()
    {
        GatewayManager.Instance.SetSpawnPosition(spawnLocation);
        GatewayManager.Instance.IsBattleScene(Enemy);
        SceneManager.LoadScene(SceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLauncherDemo : MonoBehaviour
{
    [SerializeField]
    private List<Character> players, enemies;
    [SerializeField]
    private BattleLauncher launcher;

    public void Launch()
    {
        launcher.PrepareBattle(enemies, players);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 p = collision.gameObject.transform.position;
            Scene sceneName = SceneManager.GetActiveScene();
            GatewayManager.Instance.PreviousSceneData(p, sceneName.name);
            Launch();
        }
    }
}

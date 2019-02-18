using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawner : MonoBehaviour
{
    public Character Spawn(Character character)
    {
        Character characterToSpawn = Instantiate<Character>(character, this.transform);
        return characterToSpawn;
    }
}

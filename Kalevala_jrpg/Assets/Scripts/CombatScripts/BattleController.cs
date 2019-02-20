using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance { get; set;}
    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();
    public int characterTurnIndex;
    public Spells playerSelectedSpell;
    public bool playerIsAttacking;

    [SerializeField]
    private BattleSpawner[] spawnPoints;
    
    private int actTurn;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        characters.Add(0, new List<Character>());
        characters.Add(1, new List<Character>());
    }

    public Character GetRandomPlayer()
    {
        return characters[0][Random.Range(0, characters[0].Count - 1)];
    }

    public Character GetWeakestEnemy()
    {
        Character weakestEnemy = characters[1][0];
        foreach (Character character in characters[1])
        {
            if (character.health < weakestEnemy.health)
            {
                weakestEnemy = character;
            }
        }
        return weakestEnemy;
    }

    public void StartBattle(List<Character> players, List<Character> enemies)
    {
        for (int i = 0; i < players.Count; i++)
        {
            characters[0].Add(spawnPoints[i + 3].Spawn(players[i]));
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            characters[1].Add(spawnPoints[i].Spawn(enemies[i]));
        }
    }

    void NextTurn()
    {
        actTurn = actTurn == 0 ? 1 : 0;
    }

    void NextAct()
    {
        if(characters[0].Count > 0 && characters[1].Count > 0)
        {
            if (characterTurnIndex < characters[actTurn].Count - 1)
            {
                characterTurnIndex++;
            }
            else
            {
                NextTurn();
                characterTurnIndex = 0;
                Debug.Log("turn: " + actTurn);
            }

            switch (actTurn)
            {
                case 0:
                    // do ui stuff
                    break;
                case 1:
                    StartCoroutine(PerformAct());
                    // do ui stuff
                    break;
            }
        }
        else
        {
            Debug.Log("Battle over!");
        }
    }

    IEnumerator PerformAct()
    {
        yield return new WaitForSeconds(.75f);
        if (characters[actTurn][characterTurnIndex].health > 0)
        {
            characters[actTurn][characterTurnIndex].GetComponent<Enemy>().Act();
        }
        yield return new WaitForSeconds(1f);
        NextAct();
    }

    public void SelectCharacter(Character character)
    {
        if (playerIsAttacking)
        {
            DoAttack(characters[actTurn][characterTurnIndex], character);
        }
        else if (playerSelectedSpell != null)
        {
            if (characters[actTurn][characterTurnIndex].CastSpell(playerSelectedSpell, character))
            {
                NextAct();
            }
             else 
             {
                Debug.LogWarning("Not enough mana to cast that spell!");
                }
        }
    }

    public void DoAttack(Character attacker, Character target)
    {
        target.Hurt(attacker.attackPower);
    }
}

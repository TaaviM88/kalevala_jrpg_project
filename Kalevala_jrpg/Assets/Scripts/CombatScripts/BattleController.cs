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
    public bool playerIsDefending;
    [SerializeField]
    private BattleSpawner[] spawnPoints;
    [SerializeField]
    private UIBattleController uicontroller;
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
        //pelaajat 0
        characters.Add(0, new List<Character>());
        //Viholliset 1
        characters.Add(1, new List<Character>());
        FindObjectOfType<BattleLauncher>().Launch();
        //test
        uicontroller.UpdateCharacterUI();
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
        MoveTeamMembers();  
    }

    void NextTurn()
    { 
        actTurn = actTurn == 0 ? 1 : 0;
        //MoveTeamMembers();
    }

    void NextAct()
    {

        /*GetCurrentCharacter().CharacterCanMove(true);
        GetCurrentCharacter().MoveCharacter(Vector3.right);*/
        //MoveTeamMembers();
        if (characters[0].Count > 0 && characters[1].Count > 0)
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
                    uicontroller.ToggleActionState(true);
                    uicontroller.ToggleSpellPanel(false);
                    uicontroller.BuildSpellList(GetCurrentCharacter().spells);
                    break;
                case 1:
                    StartCoroutine(PerformAct());
                    uicontroller.ToggleActionState(false);
                    break;
            }
        }
        else
        {
            Debug.Log("Battle over!");
            GatewayManager.Instance.MoveToPrevScene();
        }
    }

    IEnumerator PerformAct()
    {
        yield return new WaitForSeconds(.75f);
        if (GetCurrentCharacter().health > 0)
        {
            GetCurrentCharacter().GetComponent<Enemy>().Act();
        }
        uicontroller.UpdateCharacterUI();
        yield return new WaitForSeconds(1f);
        GoNextAct();
    }

    public void SelectCharacter(Character character)
    {
        MoveTeamMembers();
        if (playerIsAttacking)
        {
            DoAttack(GetCurrentCharacter(), character);
        }
        else if (playerSelectedSpell != null)
        {
            if (GetCurrentCharacter().CastSpell(playerSelectedSpell, character))
            {
                uicontroller.UpdateCharacterUI();
                GoNextAct();
            }
             else 
             {
                Debug.LogWarning("Not enough mana to cast that spell!");
             }
        }
        else if(playerIsDefending)
        {
            GoNextAct();
        }
        MoveTeamMembers();
    }

    public void DoAttack(Character attacker, Character target)
    {
        target.Hurt(attacker.attackPower);

        GoNextAct();
    }

    public void DoHeal(Character attacker, Character target)
    {
        target.Heal(attacker.attackPower);

        GoNextAct();
    }

    public Character GetCurrentCharacter()
    {
        return characters[actTurn][characterTurnIndex];
    }

    public void GoNextAct()
    {
        NextAct();
        //MoveTeamMembers();
    }

    public void MoveTeamMembers()
    {
        if (actTurn == 0)
        {
            GetCurrentCharacter().GetComponent<PartyMember>().Move();
        }
    }
}

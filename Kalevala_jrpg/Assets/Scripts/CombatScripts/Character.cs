using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defencePower;
    public int manaPoints;
    public List<Spells> spells;
    private bool canMove = false;

    public void Hurt(int amount)
    {
        bool dodged = Random.Range(0f, 1f) > 0.9f;
        Debug.Log("dodge " + dodged);
        //float damageAmount = /*dodged ? 0f : */amount * (100 / (100 + defencePower));
        //int damageAmount = amount - defencePower;
        float damageAmount = dodged ? 0f : amount * ((100 + defencePower) / 100);
        //float damageAmount = amount * ((100 + defencePower) / 100);
        Debug.Log("DMG: " + damageAmount);
        health = Mathf.Max(health - Mathf.RoundToInt(damageAmount), 0);
        string log = $"{characterName} takes {damageAmount} damage";
        BattleController.Instance.BattleInfo(log);
        Debug.Log("HP: " + health);
        Debug.Log("RoundedDMG: " + damageAmount);
        if(health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int healAmount = amount;
        health = Mathf.Min(health + healAmount, maxHealth);
        string log = $"{characterName} heals {amount} ";
        BattleController.Instance.BattleInfo(log);
    }

    public void Defend()
    {
        defencePower += (int)(defencePower * .33);
        //Debug.Log($"Def increased {defencePower}");
        string log = $"{characterName} Def increased {defencePower}";
        BattleController.Instance.BattleInfo(log);
    }

    public bool CastSpell(Spells spell, Character targetCharacter)
    {
        bool successful = manaPoints >= spell.manaCost;
        if(successful)
        {
            Spells spellToCast = Instantiate<Spells>(spell,transform.position, Quaternion.identity);
            manaPoints -= spell.manaCost;
            spellToCast.Cast(targetCharacter);
        }
        return successful;
    }

     public virtual void Die()
    {
        Destroy(this.gameObject);   
    }

    public void MoveCharacter(Vector3 pos)
    {
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f * Time.deltaTime);
        }
    }

    public void CharacterCanMove(bool can)
    {
        canMove = can;
    }
}

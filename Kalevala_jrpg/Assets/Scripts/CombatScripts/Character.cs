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

    public void Hurt(int amount)
    {
        bool dodged = Random.Range(0f, 1f) > 0.6f;
        float damageAmount = dodged ? 0f : amount * ((100 + defencePower) / 100);
        //int damageAmount = amount - defencePower;
        health = Mathf.Max(health - Mathf.RoundToInt(damageAmount - defencePower), 0);
        Debug.Log("HP: " + health);

        if(health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int healAmount = amount;
        health = Mathf.Min(health + healAmount, maxHealth);

    }

    public void Defend()
    {
        defencePower += (int)(defencePower * .33);
        Debug.Log($"Def increased {defencePower}");
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
}

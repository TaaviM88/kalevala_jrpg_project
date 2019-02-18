using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Act()
    {
        int dieRoll = Random.Range(0, 2);
        switch(dieRoll)
        {
            case 0:
                Defend();
                break;
            case 1:
                Spells spellsToCast = GetRandomSpell();
                if(spellsToCast.spellType == Spells.SpellType.Heal)
                {

                }

                if(!CastSpell(spellsToCast,null))
                {

                }
                break;
            case 2:
                //attack
                break;
        }
    }

    Spells GetRandomSpell()
    {
        return spells[Random.Range(0, spells.Count - 1)];
    }

    public override void Die()
    {
        base.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Act()
    {
        int dieRoll = Random.Range(0, 2);
        Character target = BattleController.Instance.GetRandomPlayer();

        switch(dieRoll)
        {
            case 0:
                Defend();
                break;
            case 1:
                Spells spellsToCast = GetRandomSpell();
                if(spellsToCast.spellType == Spells.SpellType.Heal)
                {
                    target = BattleController.Instance.GetWeakestEnemy();
                }

                if(!CastSpell(spellsToCast,target))
                {
                    BattleController.Instance.DoAttack(this, target);
                }
                break;
            case 2:
                BattleController.Instance.DoAttack(this, target);
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
        BattleController.Instance.characters[1].Remove(this);
    }
}

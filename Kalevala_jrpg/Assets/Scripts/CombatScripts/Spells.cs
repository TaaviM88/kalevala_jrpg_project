using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public string spellName;
    public int power;
    public int manaCost;
    public enum SpellType { Attack, Heal}
    public SpellType spellType;

    private Vector3 targetPosition;

    private void Update()
    {
        if(targetPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, .15f);
            if(Vector3.Distance(transform.position,targetPosition) < .25f)
            {
                Destroy(this.gameObject, 1);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Cast(Character target)
    {
        targetPosition = target.transform.position;
        if(spellType == SpellType.Attack)
        {
            target.Hurt(power);
        }
        else if(spellType == SpellType.Heal)
        {
            target.Heal(power);
            Debug.Log("Healing!");
        }
    }

}

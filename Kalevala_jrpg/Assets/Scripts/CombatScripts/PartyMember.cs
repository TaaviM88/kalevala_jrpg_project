using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : Character
{
    private Vector3[] movementDirections = new Vector3[] { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
    private Vector3 spawnPosition;
    public enum PartyMemberName {Ilmarinen, Lemminkainen, Vainamoinen }
    public PartyMemberName partyMemberName;
    private void Start()
    {
        spawnPosition = transform.position;

        if( 0 < PlayersCombatData.Instance.GetCurrentHP(partyMemberName))
        {
            health = PlayersCombatData.Instance.GetCurrentHP(partyMemberName);
            Debug.Log("Haetaan hp:t " + partyMemberName + "HP on nyt " + health);
        }
        else
        {
            health = maxHealth;
        }

        if (0 < PlayersCombatData.Instance.GetCurrentMP(partyMemberName))
        {
            manaPoints = PlayersCombatData.Instance.GetCurrentMP(partyMemberName);
            Debug.Log("Haetaan mp:t " + partyMemberName);
        }

        BattleController.Instance.UpdateUIStats();
    }

    public void Move()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition == spawnPosition)
        {
            Vector3 destination = currentPosition + movementDirections[3];
            StartCoroutine(this.MoveTo(destination));
        }        
        else
        {
            StartCoroutine(this.MoveTo(spawnPosition));
        }
    }


    public IEnumerator MoveTo(Vector3 targetPosition, float delay = 0f)
    {
        while (targetPosition != new Vector3(transform.position.x, transform.position.y, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delay);

    }

    public override void Die()
    {
        base.Die();
        BattleController.Instance.characters[0].Remove(this);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    private Vector3[] movementDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private Vector3 spawnPosition;

    [SerializeField]
    private bool wander;

    public void Wander()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition == spawnPosition)
        {
            int roll = Random.Range(0, 3);
            Vector3 destination = currentPosition + movementDirections[roll];
            StartCoroutine(this.MoveTo(destination, Wander, Random.Range(2, 5)));
        }
        else
        {
            StartCoroutine(this.MoveTo(spawnPosition, Wander, Random.Range(2, 5)));
        }
    }
}

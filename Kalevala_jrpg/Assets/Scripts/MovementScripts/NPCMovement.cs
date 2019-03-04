using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
    private Vector3[] movementDirections = new Vector3[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        Wander();
    }

    public void Wander()
    {
        Vector3 currentPosition = transform.position;
        if(currentPosition == spawnPosition)
        {
            int roll = Random.Range(0, 3);
            Vector2 destination = currentPosition + movementDirections[roll];
            StartCoroutine(this.MoveTo(destination, Wander, Random.Range(2, 5)));
        }
        else
        {
            StartCoroutine(this.MoveTo(spawnPosition, Wander, Random.Range(2, 5)));
        }
    }
}

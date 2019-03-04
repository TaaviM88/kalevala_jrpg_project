using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
    private Vector3[] movementDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private Vector3 spawnPosition;

    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private bool wander;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        //Laitetaan halutaanko että npc liikkuu vai ei?
        if (wander)
        {
            Wander();
        }
    }

    public void Wander()
    {
        Vector3 currentPosition = transform.position;
        if(currentPosition == spawnPosition)
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

    public void StartDialogue()
    {
        dialogue.StartDialogue(dialogueData.dialogue);
    }
}

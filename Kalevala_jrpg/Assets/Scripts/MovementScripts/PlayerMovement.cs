using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private void Update()
    {
        this.Move(new Vector3(Input.GetAxisRaw("Horizontal"), transform.position.y ,Input.GetAxisRaw("Vertical")));
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("Fire1") && other.GetComponent<NPCMovement>() != null)
        {
            other.GetComponent<NPCMovement>().StartDialogue();
        }else if(Input.GetButtonDown("Fire1") && other.GetComponent<Gateway>() != null) {
            other.GetComponent<Gateway>().ChangeScene();
        }
    }

    public void DisablePlayer()
    {
        this.gameObject.SetActive(false);
    }
}

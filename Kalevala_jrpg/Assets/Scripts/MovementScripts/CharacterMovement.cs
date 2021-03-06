﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    //private Rigidbody2D _rb;
    private Rigidbody _rb;
    [SerializeField]
    private float movementSpeed;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector3 inputVector)
    {
        if(inputVector.x != Vector3.zero.x)
        {
            sprite.flipX = inputVector.normalized.x < 0;
        }
        inputVector -= inputVector.normalized * movementSpeed;
        _rb.velocity = inputVector;
    }

    public IEnumerator MoveTo(Vector3 targetPosition,System.Action callback, float delay = 0f)
    {
        while(targetPosition != new Vector3(transform.position.x,transform.position.y, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delay);
        callback();
    }

    public void TeleportTo(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}

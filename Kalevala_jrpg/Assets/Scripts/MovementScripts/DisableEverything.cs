using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEverything : MonoBehaviour
{
    public void ToggleOn(bool t)
    {
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(t);
    }
}


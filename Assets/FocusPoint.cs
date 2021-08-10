using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPoint : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = target.transform.position;
    }
}

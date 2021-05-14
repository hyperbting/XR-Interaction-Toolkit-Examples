﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    public Renderer assignedRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        assignedRenderer.material.SetColor("_BaseColor", Color.red);
        print("sword on collision enter");
    }
    
    private void OnCollisionStay(Collision other)
    {
        assignedRenderer.material.SetColor("_BaseColor", Color.yellow);
        print("sword on collision stay");
    }
    private void OnCollisionExit(Collision other)
    {
        assignedRenderer.material.SetColor("_BaseColor", Color.white);
        print("sword on collision exit");
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        assignedRenderer.material.SetColor("_BaseColor", Color.magenta);
        print("sword on trigger enter");
    }
    
    private void OnTriggerStay(Collider other)
    {
        print(other.name);
        assignedRenderer.material.SetColor("_BaseColor", Color.cyan);
        print("sword on trigger stay");
    }
    
    private void OnTriggerExit(Collider other)
    {
        print(other.name);
        assignedRenderer.material.SetColor("_BaseColor", Color.blue);
        print("sword on trigger exit");
    }
}

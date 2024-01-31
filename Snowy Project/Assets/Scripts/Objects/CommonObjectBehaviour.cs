using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apricity{

public abstract class CommonObjectBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D col){}

    protected virtual void OnTriggerEnter2D(Collider2D col){}

    public abstract void action();
}
}
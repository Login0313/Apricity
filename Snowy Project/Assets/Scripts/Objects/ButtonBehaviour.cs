using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apricity{

public class ButtonBehaviour : CommonObjectBehaviour
{

    public GameObject objectToBeActivated;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            action();
            gameObject.SetActive(false);
        }
    }

    public override void action(){
        objectToBeActivated.GetComponent<CommonObjectBehaviour>().action();
    }
}
}
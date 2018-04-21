using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        var obj= collision.gameObject.GetComponent<ITakeHit>();
        if (obj!=null)
        {
            obj.TakeHit();
        }
        
        Destroy(gameObject);
    }
}

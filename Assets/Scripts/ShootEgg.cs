using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEgg : MonoBehaviour
{
    [SerializeField]
    private GameObject egg;
    [SerializeField]
    private float shootSpeed;

    public void Shoot()
    {
        var projectile = Instantiate(egg, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);
        
    }
   
}

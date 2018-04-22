using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var obj= collision.gameObject.GetComponent<ITakeHit>();
        if (obj!=null)
        {
            audioSource.PlayOneShot(clip);
            obj.TakeHit();
        }
        
        Destroy(gameObject);
    }
}

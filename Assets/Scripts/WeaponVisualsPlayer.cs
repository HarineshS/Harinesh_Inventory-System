using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualsPlayer : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem muzzleEffect;
    private AudioSource audioSource;
    public AudioClip shootAudio; 
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        
        
    }

    public void PlayVisuals()
    {
        //muzzleEffect.GetComponentInChildren<ParticleSystem>().Play();
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(shootAudio);
        

    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}

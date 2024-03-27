using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualsPlayer : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void PlayVisuals()
    {
        StartCoroutine(PlayAnimation());

    }
    IEnumerator PlayAnimation()
    {
        // Enable the animator
        animator.enabled = true;

        // Play the animation
        //animator.Play("shoot");

        // Wait until the animation is complete
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Disable the animator after the animation is complete
        animator.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

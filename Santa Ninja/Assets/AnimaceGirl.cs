using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaceGirl : MonoBehaviour
{
    public MobAI mobAI;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mobAI.mobIsWaiting)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }
    }
}

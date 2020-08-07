using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = transform.Find("CatImage").GetComponent<Animator>();
    }

    public void RunRight()
    {
        _animator.SetInteger("STATE", 1);
    }

    public void RunLeft()
    {
        _animator.SetInteger("STATE", 2);
    }

    public void RunTumble()
    {
        _animator.SetInteger("STATE", 3);
    }

    public void RunJump()
    {
        _animator.SetInteger("STATE", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

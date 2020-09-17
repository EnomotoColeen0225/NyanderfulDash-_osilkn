using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private AudioClip Tumble = null;
    [SerializeField]
    private AudioClip Run = null;
    [SerializeField]
    private AudioClip Jump = null;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _animator = transform.Find("CatImage").GetComponent<Animator>();
    }

    public void RunRight()
    {
        audioSource.PlayOneShot(Run);
        _animator.SetInteger("STATE", 1);
    }

    public void RunLeft()
    {
        audioSource.PlayOneShot(Run);
        _animator.SetInteger("STATE", 2);
    }

    public void RunTumble()
    {
        audioSource.PlayOneShot(Tumble);
        _animator.SetInteger("STATE", 3);
    }

    public void RunJump()
    {
        _animator.SetInteger("STATE", 4);
        StartCoroutine("PlayACJump");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayACJump()
    {
        for(int i = 0; i< 4; i++)
        {
            audioSource.PlayOneShot(Jump);
            yield return new WaitForSeconds(1);
        }
    }
}

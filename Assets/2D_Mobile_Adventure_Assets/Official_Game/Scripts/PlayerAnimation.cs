using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private Animator _swordAnim;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();    
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    
    public void Move(float move) 
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }
}

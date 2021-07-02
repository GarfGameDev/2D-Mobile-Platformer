using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpSpeed = 5.0f;
    private float _movementSpeed = 4.0f;
    private Rigidbody2D _rigidbody;
    private bool _resetJump = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    } 

    void Update()
    {
        Movement();

    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        }

        _rigidbody.velocity = new Vector2(move * _movementSpeed, _rigidbody.velocity.y);

        _playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector3.down, 0.6f, 1 << 8);

        if (hitInfo.collider != null)
        {
            if (_resetJump == false) 
            {
                return true;
            }
            
        }

        return false;
    }

    void Flip (bool facingRight) 
    {
        if (facingRight == true)
        {
            _playerSprite.flipX = false;
        }
        else if (facingRight == false)
        {
            _playerSprite.flipX = true;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

}

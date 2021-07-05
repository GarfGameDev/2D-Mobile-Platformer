using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpSpeed = 5.0f;
    private float _movementSpeed = 4.0f;
    private Rigidbody2D _rigidbody;
    private bool _resetJump = false;

    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    } 

    void Update()
    {
        Movement();

        if  (Input.GetMouseButtonDown(0) && IsGrounded() == true) 
        {
            _playerAnim.Attack();
        }

    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();
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
            _playerAnim.Jump(true);
            StartCoroutine(ResetJumpRoutine());
        }

        _rigidbody.velocity = new Vector2(move * _movementSpeed, _rigidbody.velocity.y);

        _playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector3.down, 1f, 1 << 8);

        if (hitInfo.collider != null)
        {
            if (_resetJump == false) 
            {
                _playerAnim.Jump(false);
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
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (facingRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.2f);
        _resetJump = false;
    }

}

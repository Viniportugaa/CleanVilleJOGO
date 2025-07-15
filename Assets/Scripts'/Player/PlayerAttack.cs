using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    //[SerializeField] private AudioClip fireballSound;
    public InputAction playerControls;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    //PhotonView view;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        //view = GetComponent<PhotonView>();

    }

    private void Update()
    {
        //if (view.IsMine)
        //{

            //if (Input.GetButton("Fire1") && cooldownTimer > attackCooldown && playerMovement.canAttack() && Time.timeScale > 0)
                //Attack();

            //cooldownTimer += Time.deltaTime;
        //}
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            //SoundManager.instance.PlaySound(fireballSound);
            anim.SetTrigger("attack");

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
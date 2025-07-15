using UnityEngine;
using System.Collections;

public class AnimatedPlatform : MonoBehaviour
{
    public float disappearDelay = 0.5f;  // wait after animation starts
    public float reappearDelay = 3f;     // how long to stay hidden

    //private Animator animator;
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
       // animator = GetComponent<Animator>();
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(DisappearAndReappear());
        }
    }

    private IEnumerator DisappearAndReappear()
    {
        //animator.SetTrigger("Disappear");

        yield return new WaitForSeconds(disappearDelay);

        platformCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(reappearDelay);

        platformCollider.enabled = true;
        spriteRenderer.enabled = true;

        //animator.Play("Idle");
    }
}

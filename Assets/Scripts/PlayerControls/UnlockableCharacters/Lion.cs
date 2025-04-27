using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour, ICharacterActions
{
    public AudioSource lionSound;
    //Lion attack: Slash
    private bool isAttacking;
    private int damage = 1;
    private float attackWait = 0.5f;
    [SerializeField] private GameObject paw;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem slashVFX;

    void Start()
    {
        lionSound = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StartCoroutine(TimeAttack());
    }


    private void OnTriggerEnter(Collider collision)
    {
        HitEnemy(collision);
    }


    void HitEnemy(Collider _collision)
    {
        if(_collision.gameObject.GetComponent<EnemyHealth>() != null && isAttacking) 
        {
            //Debug.Log("enemy hit " + _collision.gameObject.name);
            Attack(_collision.gameObject.GetComponent<EnemyHealth>());
        }
    }

    public void Attack(EnemyHealth _enemy)
    {
        _enemy.TakeDamage(damage);
    }

    private IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(attackWait);
        isAttacking = true;
        lionSound.Play();
        animator.SetTrigger("Slash");
        slashVFX.Play();
        StartCoroutine(TurnOffAttack());
    }

    IEnumerator TurnOffAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        isAttacking= false;

        StartCoroutine(TimeAttack());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lion : MonoBehaviour, ICharacterActions
{
    //Lion attack: Slash
    private bool isAttacking;
    private int damage = 1;
    private float attackWait = 0.5f;
    [SerializeField] private GameObject paw;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem slashVFX;

    private bool isInGameScene;
    public AudioSource slashSound;

    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";
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
        if (_collision.gameObject.GetComponent<EnemyHealth>() != null && isAttacking)
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
        animator.SetTrigger("Slash");
        if (isInGameScene == true)
        {
            slashVFX.Play();
            slashSound.Play();
        }

        StartCoroutine(TurnOffAttack());
    }

    IEnumerator TurnOffAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        isAttacking = false;

        StartCoroutine(TimeAttack());
    }

}
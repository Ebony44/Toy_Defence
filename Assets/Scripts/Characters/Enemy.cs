using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IDamageable
{
    public AttackRadius attackRadius;
    public Animator animator;
    private Coroutine mLookCoroutine;

    public EnemyMovement movement;
    public NavMeshAgent agent;
    public EnemySO enemyScriptableObject;
    public int health = 100;

    public Material enemyMat;
    public Color originalColor = Color.gray;

    private const string ATTACK_TRIGGER = "Attack";


    private void Awake()
    {
        attackRadius.OnAttack += OnAttack;

        
    }

    private void OnAttack(IDamageable target)
    {
        var tempString = ATTACK_TRIGGER;
        if(animator != null)
        {
            animator.SetTrigger(tempString);
        }
        

        if (mLookCoroutine != null)
        {
            StopCoroutine(mLookCoroutine);
        }

        mLookCoroutine = StartCoroutine(LookAt(target.GetTransform()));

    }

    private IEnumerator LookAt(Transform targetTrans)
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetTrans.position - transform.position);
        yield break;
    }



    public virtual void OnEnable()
    {
        SetupAgentFromConfiguration();

        AssignMat();
        // assign new material
    }

    private void AssignMat()
    {
        var tempMeshRenderer = GetComponentInChildren<MeshRenderer>();
        if (tempMeshRenderer != null)
        {
            if (tempMeshRenderer.material != null)
            {
                tempMeshRenderer.material.color = originalColor;
                enemyMat = tempMeshRenderer.material;

                // TODO: should replace decal or something
                if (enemyMat != null)
                {
                    enemyMat = new Material(enemyMat);
                    tempMeshRenderer.material = enemyMat;

                }
            }
        }
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }
    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    Agent.enabled = false;
    //}

    public virtual void SetupAgentFromConfiguration()
    {
        agent.acceleration = enemyScriptableObject.Acceleration;
        agent.angularSpeed = enemyScriptableObject.AngularSpeed;
        agent.areaMask = enemyScriptableObject.AreaMask;
        agent.avoidancePriority = enemyScriptableObject.AvoidancePriority;
        agent.obstacleAvoidanceType = enemyScriptableObject.ObstacleAvoidanceType;
        agent.radius = enemyScriptableObject.Radius;
        agent.speed = enemyScriptableObject.Speed;
        agent.stoppingDistance = enemyScriptableObject.StoppingDistance;

        movement.updateRate = enemyScriptableObject.AIUpdateInterval;

        health = enemyScriptableObject.Health;

        attackRadius.RadiusCollider.radius = enemyScriptableObject.AttackRadius;
        attackRadius.AttackDelay = enemyScriptableObject.AttackDelay;
        attackRadius.DamageValue = enemyScriptableObject.Damage;


    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ChangeMatRoutine(0.2f));
            //if(enemyMat != null)
            //{
            //    enemyMat.color = Color.red;
            //}
        }

    }

    public IEnumerator ChangeMatRoutine(float changingTime)
    {
        
        if(enemyMat != null)
        {
            if(enemyMat.HasProperty("_Color"))
            {
                enemyMat.color = Color.red;
                yield return new WaitForSeconds(changingTime);
                enemyMat.color = originalColor;
            }
        }
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}

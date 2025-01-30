using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Animations.Rigging;
using Sirenix.OdinInspector;

public class Tower : MonoBehaviour
{

    public AttackRadius attackRadius;
    public Animator animator;
    private Coroutine mLookCoroutine;

    // public EnemyMovement Movement;
    public NavMeshAgent agent;
    public TowerSO towerScriptableObject;
    public int health = 100;

    public Material towerMat;
    public Color originalColor = Color.white;

    private const string ATTACK_TRIGGER = "Attack";
    private const string ATTACK_BOOL = "isAttacking";

    public Transform lookingTarget;
    public RigBuilder rigBuilder;
    public MultiAimConstraint multiAimConstraint;
    IDamageable mCurrentTarget;

    private void Awake()
    {
        attackRadius.OnAttack += OnAttack;
        attackRadius.OnStopAttack += ClearLookingTarget;
        attackRadius.OnStopAttack += StopAttack;
        // multiAimConstraint.weight = 0f;
    }
    

    private void OnAttack(IDamageable target)
    {
        Debug.Log("[Tower][OnAttack]");
        if (animator != null)
        {
            animator.SetTrigger(ATTACK_TRIGGER);
        }

        //if (mLookCoroutine != null)
        //{
        //    StopCoroutine(mLookCoroutine);
        //}

        if(mCurrentTarget == null)
        {
            mLookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
            mCurrentTarget = target;
        }
        

    }

    private IEnumerator LookAt(Transform targetTrans)
    {
        // Quaternion lookRotation = Quaternion.LookRotation(targetTrans.position - transform.position);
        if(multiAimConstraint != null)
        {
            Debug.Log("[LookAt], multiAimConstraint is not null");
            lookingTarget.position = targetTrans.position;
            multiAimConstraint.weight = 1f;
            if (animator != null)
            {
                Debug.Log("[LookAt], ATTACK_BOOL true");
                animator.SetBool(ATTACK_BOOL, true);
            }

            while (multiAimConstraint.weight > 0f)
            {
                lookingTarget.position = targetTrans.position;
                yield return null;
            }
            Debug.Log("[LookAt], multiAimConstraint.weight is 0");
            

        }
        yield break;
    }
    private void ClearLookingTarget()
    {
        Debug.Log("[ClearLookingTarget]");
        if (multiAimConstraint != null)
        {
            // multiAimConstraint.data.sourceObjects.Clear();
            multiAimConstraint.weight = 0f;
            if (animator != null)
            {
                Debug.Log("[ClearLookingTarget], ATTACK_BOOL false");
                animator.SetBool(ATTACK_BOOL, false);
            }
        }
        if(mLookCoroutine != null)
        {
            StopCoroutine(mLookCoroutine);
        }
    }
    private void StopAttack()
    {
        mCurrentTarget = null;
    }

    [Button]
    public void ShowVariable()
    {
        var temp = multiAimConstraint.data.sourceObjects;
        Debug.Log("[ShowVariable]");
    }

    [Button]
    public void RebuildRig()
    {
        rigBuilder.Build();
        Debug.Log("[RebuildRig]");
    }

    
}

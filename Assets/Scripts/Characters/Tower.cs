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

    public Transform lookingTarget;
    public RigBuilder rigBuilder;
    public MultiAimConstraint multiAimConstraint;
    IDamageable mCurrentTarget;

    private void Awake()
    {
        attackRadius.OnAttack += OnAttack;
    }

    private void OnAttack(IDamageable target)
    {
        if(animator != null)
        {
            animator.SetTrigger(ATTACK_TRIGGER);
        }

        if (mLookCoroutine != null)
        {
            StopCoroutine(mLookCoroutine);
        }

        if(mCurrentTarget == null)
        {
            mLookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
            mCurrentTarget = target;
        }
        else if(mCurrentTarget != target)
        {

        }
        

    }

    private IEnumerator LookAt(Transform targetTrans)
    {
        // Quaternion lookRotation = Quaternion.LookRotation(targetTrans.position - transform.position);
        if(multiAimConstraint != null)
        {
            Debug.Log("[LookAt], multiAimConstraint is not null");
            // rigBuilder.Build();
            var tempSourceObjects = multiAimConstraint.data.sourceObjects;
            tempSourceObjects.Add(new WeightedTransform(targetTrans, 1f));
            // var temp = multiAimConstraint.data.sourceObjects;
            //temp.Add(new WeightedTransform(targetTrans, 1f));
            multiAimConstraint.data.sourceObjects = tempSourceObjects;

            rigBuilder.Build();
            
        }
        yield break;
    }
    private void ClearLookingTarget()
    {
        if(multiAimConstraint != null)
        {
            multiAimConstraint.data.sourceObjects.Clear();
        }
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

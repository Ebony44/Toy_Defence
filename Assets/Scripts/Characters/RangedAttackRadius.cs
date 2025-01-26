using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class RangedAttackRadius : AttackRadius
{

    public NavMeshAgent agent;
    public Bullet bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    public LayerMask layerMask;
    
    public ObjectPool<Bullet> bulletPool;

    [SerializeField] private float mSphereCastRadius = 0.2f;
    private RaycastHit mHit;
    private IDamageable mTargetDamageable;



    protected override void Awake()
    {
        base.Awake();
        bulletPool = new ObjectPool<Bullet>(CreateBullet,OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: 64);
    }

    protected override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        yield return wait;

        Debug.Log("[RangedAttackRadius], attack coroutine started");
        int currentWhileCycle = 0;
        while (Damageables.Count > 0)
        {
            Debug.Log("[RangedAttackRadius], attack coroutine while cycle: " + currentWhileCycle);
            for (int i = 0; i < Damageables.Count; i++)
            {
                //if (HasLineOfSightTo(Damageables[i].GetTransform()))
                //{
                //    mTargetDamageable = Damageables[i];
                //    OnAttack?.Invoke(mTargetDamageable);
                //    agent.enabled = false;
                //    Debug.Log("Has line of sight to target");
                    
                //    break;
                //}

                mTargetDamageable = Damageables[i];
                OnAttack?.Invoke(mTargetDamageable);
                agent.enabled = false;
                Debug.Log("Has line of sight to target");
                break;

            }

            if(mTargetDamageable != null)
            {
                Debug.Log("mTargetDamageable is not null");
                // Bullet bullet = CreateBullet();
                Bullet bullet = bulletPool.Get();
                bullet.transform.position = transform.position + bulletSpawnOffset;
                bullet.transform.rotation = agent.transform.rotation;
                bullet.rb.AddForce((mTargetDamageable.GetTransform().position - bullet.transform.position).normalized * bullet.physicsMoveSpeed,
                    ForceMode.VelocityChange);

                //bullet.rb.AddForce(agent.transform.forward * bullet.physicsMoveSpeed,
                //    ForceMode.VelocityChange);

                // bullet.SetTarget(mTargetDamageable.GetTransform());
                bullet.gameObject.SetActive(true);
            }
            else
            {
                agent.enabled = true; // no target in line of sight
            }

            yield return wait;

            //if(mTargetDamageable == null || HasLineOfSightTo(mTargetDamageable.GetTransform()))
            //{
            //    agent.enabled = true;
            //}
            Damageables.RemoveAll(DisabledDamageables);

            Debug.Log("[RangedAttackRadius], Damageables.Count " + Damageables.Count);
            currentWhileCycle++;

        }

        agent.enabled = true;
        mAttackCoroutine = null;

    }

    private bool HasLineOfSightTo(Transform target)
    {
        Debug.Log("Checking line of sight");
        // raycasthit
        RaycastHit hit;
        var direction = ((target.position + bulletSpawnOffset) - (transform.position + bulletSpawnOffset)).normalized;
        var bIsCastHit = Physics.SphereCast(transform.position + bulletSpawnOffset, mSphereCastRadius, direction, out hit, RadiusCollider.radius, layerMask);
        Debug.Log("Has line of sight to target: " + bIsCastHit);
        if (bIsCastHit)
        {
            IDamageable damageable;
            if(hit.collider.TryGetComponent(out damageable))
            {
                return damageable.GetTransform() == target;
            }
            //var currentTarget = hit.collider.GetComponent<IDamageable>();
            //if (currentTarget != null)
            //{
            //    if()
            //    return true;
            //}
        }
        return false;

    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if(mAttackCoroutine == null)
        {
            agent.enabled = true;
        }

    }


    ///
    ///
    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

        bullet.SetPool(bulletPool);
        bullet.bIsReleased = false;

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        //bullet.transform.position = transform.position + bulletSpawnOffset;
        //bullet.transform.rotation = agent.transform.rotation;

        bullet.gameObject.SetActive(true);
        bullet.bIsReleased = false;
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }


}

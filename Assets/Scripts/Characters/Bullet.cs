using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float autoDestroyTime = 4f;
    public float physicsMoveSpeed = 5f;
    public int damage = 1;
    public Rigidbody rb;

    private const string DISABLE_METHOD_NAME = "Disable";

    private ObjectPool<Bullet> mPool;

    public bool bIsReleased = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, autoDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Bullet], OnTriggerEnter, entered collider is ");
        IDamageable damageable;
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            Debug.Log(other.name);
            damageable.TakeDamage(damage);
            Disable();
        }
        
    }

    public void Disable()
    {
        if(bIsReleased)
        {
            return;
        }
        CancelInvoke(DISABLE_METHOD_NAME);
        rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
        // how to check this object is already released to mPool?
        bIsReleased = true;
        mPool.Release(this);
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        mPool = pool;
    }

}

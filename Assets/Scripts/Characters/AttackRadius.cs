using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{

    public SphereCollider RadiusCollider; // use as radius
    protected List<IDamageable> Damageables = new List<IDamageable>();
    public float AttackDelay = 0.4f;
    public LayerMask attackLayer;

    public int DamageValue;

    public delegate void AttackEvent(IDamageable Target);
    public AttackEvent OnAttack;
    public Action OnStopAttack;

    protected Coroutine mAttackCoroutine;


    protected virtual void Awake()
    {
        RadiusCollider = GetComponent<SphereCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("[AttackRadius], OnTriggerEnter, entered collider is ");
        if ((1 << other.gameObject.layer & attackLayer.value) <= 0)
        {
            Debug.Log("Layer is not in attackLayer, if statement result is " + (1 << other.gameObject.layer & attackLayer.value));
            return;
        }

        // reverse below condition
        if( (1 << other.gameObject.layer & attackLayer.value) > 0)
        {
            Debug.Log("Layer is in attackLayer, if statement result is " + (1 << other.gameObject.layer & attackLayer.value));
        }
        if ((other != null))
        {
            Debug.Log(other.name);
        }
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Add(damageable);
            if (mAttackCoroutine == null)
            {
                Debug.Log("[AttackRadius], OnTriggerEnter, start mAttackCoroutine, gameobject is " + other.name);
                mAttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        Debug.Log("[AttackRadius], OnTriggerExit, entered collider is ");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if (Damageables.Count == 0)
            {
                StopCoroutine(mAttackCoroutine);
                mAttackCoroutine = null;
                // AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    protected virtual IEnumerator Attack()
    {
        Debug.Log("[AttackRadius], Attack, entered Attack");
        WaitForSeconds wait = new WaitForSeconds(AttackDelay);

        yield return wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(this.transform.position, damageableTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                }
            }

            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(DamageValue);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;
            yield return wait;
            Damageables.RemoveAll(DisabledDamageables);

        }

        mAttackCoroutine = null;
        StopAttack();

    }

    protected virtual void StopAttack()
    {
        OnStopAttack?.Invoke();
    }
    protected bool DisabledDamageables(IDamageable paramDamageable)
    {
        return paramDamageable != null && paramDamageable.GetTransform().gameObject.activeSelf == false;
    }

}

using UnityEngine;
using System.Collections;

public class CorePoint : MonoBehaviour, IDamageable
{
    public CorePointSO coreScriptableObject;
    public int health = 100;
    public Material coreMat;
    public Color originalColor = Color.gray;

    public Transform GetTransform()
    {
        return transform;
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

        if (coreMat != null)
        {
            if (coreMat.HasProperty("_Color"))
            {
                coreMat.color = Color.red;
                yield return new WaitForSeconds(changingTime);
                coreMat.color = originalColor;
            }
        }
    }
    private void AssignMat()
    {
        var tempMeshRenderer = GetComponentInChildren<MeshRenderer>();
        if (tempMeshRenderer != null)
        {
            if (tempMeshRenderer.material != null)
            {
                tempMeshRenderer.material.color = originalColor;
                coreMat = tempMeshRenderer.material;

                // TODO: should replace decal or something
                if (coreMat != null)
                {
                    coreMat = new Material(coreMat);
                    tempMeshRenderer.material = coreMat;

                }
            }
        }
    }


    public virtual void SetupStatFromConfiguration()
    {
        health = coreScriptableObject.Health;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupStatFromConfiguration();
        AssignMat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

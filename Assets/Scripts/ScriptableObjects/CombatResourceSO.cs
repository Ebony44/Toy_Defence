using UnityEngine;

[CreateAssetMenu(fileName = "Resource Configuration", menuName = "ScriptableObject/Resource Configuration")]
public class CombatResourceSO : SerializableScriptableObject
{
    // need to change below variables to readonly?
    [Tooltip("The initial Resources")]
    [SerializeField] private float mMaxResource;
    [SerializeField] private float mCurrentResource;
    [SerializeField] private float mInitialGainRate;


    [SerializeField] private EResourceType mResourceType;


    private enum EResourceType
    {
        Iron,
        //Health,
        //Mana,
        //Stamina
    }

    public float MaxResource => mMaxResource;
    
    public float CurrentResource => mCurrentResource;

    public float InitialGainRate => mInitialGainRate;

    public void SetCurrentResource(float value)
    {
        mCurrentResource = value;
    }
    public void SetMaxResource(float value)
    {
        mMaxResource = value;
    }

    public void GainResource(float value)
    {
        mCurrentResource += value;
        if (mCurrentResource > mMaxResource)
        {
            mCurrentResource = mMaxResource;
        }
    }
    public void LoseResource(float value)
    {
        mCurrentResource -= value;
        if (mCurrentResource < 0)
        {
            mCurrentResource = 0;
        }
    }
}

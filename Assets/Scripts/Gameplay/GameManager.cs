using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Combat Resources")]
    [SerializeField] private CombatResourceSO mIronResource;

    [Header("Broadcasting on")]
    [SerializeField] private IntEventChannelSO _UIUpdateNeeded = default;

    public bool bIsGamePlaying = false;
    public float updateRate = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GainIronResource()
    {
        while(bIsGamePlaying)
        {
            mIronResource.GainResource(mIronResource.InitialGainRate);
            _UIUpdateNeeded.RaiseEvent(0);
            yield return new WaitForSeconds(updateRate);
        }
    }

}

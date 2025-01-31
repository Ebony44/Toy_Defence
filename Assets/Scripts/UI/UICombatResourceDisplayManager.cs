using UnityEngine;
using UnityEngine.UI;

public class UICombatResourceDisplayManager : MonoBehaviour
{
    [SerializeField] private CombatResourceSO[] currentResources; //watching this variable
    // [SerializeField] private Image[] resourceImages;
    [SerializeField] private UICombatResourceDisplay[] resourceDisplay;

    [Header("Listening to")]
    [SerializeField] private IntEventChannelSO _UIUpdateNeeded = default; //The player will broadcast this event when the resource changes

    private void OnEnable()
    {
        _UIUpdateNeeded.OnEventRaised += UpdateResource;
    }
    private void OnDestroy()
    {
        _UIUpdateNeeded.OnEventRaised -= UpdateResource;
    }

    public void UpdateResource(int resourceIndex)
    {

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

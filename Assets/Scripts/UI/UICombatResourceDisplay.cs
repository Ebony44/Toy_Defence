using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICombatResourceDisplay : MonoBehaviour
{
    public Image resourceImage;
    public TextMeshProUGUI resourceText;

    //[Header("Listening to")]
    //[SerializeField] private IntEventChannelSO _UIUpdateNeeded = default; //The player will broadcast this event when the resource changes

    public void UpdateResource(float currentResourceAmount)
    {
        Debug.Log("Updating resource amount to " + currentResourceAmount);
        resourceText.text = currentResourceAmount.ToString();
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

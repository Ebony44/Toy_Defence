using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static AttackRadius;
using static GameInput;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : SerializableScriptableObject, IPlayerActions
{
    // Gameplay binding
    public event UnityAction attackEvent = delegate { };
    public event UnityAction construct1KeyPressEvent = delegate { };

    // changed into input system binding
    private GameInput mGameInput;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(this);
        //}
    }
    private void OnEnable()
    {
        Debug.Log("InputReader enabled");
        if (mGameInput == null)
        {
            Debug.Log("assign mGameInput");
            mGameInput = new GameInput();
            mGameInput.Player.SetCallbacks(this);

            mGameInput.Player.Enable();
        }
    }
    private void OnDisable()
    {
        mGameInput.Player.Disable();
    }




    public void OnMove(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Debug.Log("[OnAttack], Attack button pressed");
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("[OnAttack], Attack button performed");
                attackEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                Debug.Log("[OnAttack], Attack button canceled");
                // AttackCanceledEvent.Invoke();
                break;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }

    public void OnStruct_1_Button(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("[OnStruct_1_Button], OnStruct_1_Button performed");
                construct1KeyPressEvent.Invoke();
                break;
            //case InputActionPhase.Canceled:
            //    Debug.Log("[OnStruct_1_Button], OnStruct_1_Button canceled");
            //    // AttackCanceledEvent.Invoke();
            //    break;
        }
    }
}

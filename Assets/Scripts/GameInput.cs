using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }


    public Vector2 GetMovementVectorNormalized()
    {
        // old input system
        //Vector2 inputVector = new Vector2(0, 0);
        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.y = +1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.y = -1;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x = +1;
        //}


        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        // or you can also normalize it in the PlayerInputController, where you set the binds
        // in processor, there is an option to Normalize


        Debug.Log(inputVector);

        return inputVector;


        // Now we just made a PlayerInputActions and we can add it directly to the player
        // by clicking on add, adding a new Player Input, and then in actions, lets add the PlayerInputActions that we just created



    }
}

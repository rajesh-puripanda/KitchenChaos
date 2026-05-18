using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}


    private Vector3 lastInteractDir;

    private bool isWalking;
    // to return whether walking or not, for the animator

    //  public float moveSpeed = 7f;
    //    However this being public, exposes this to the entire file, and you can change it from any other files unintentionally, althought having it public, 
    //    helps you change and tweak the value from the editor itself, in production ready code, that is not advisable

    //    Instead you can just use, 
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    // Update is called once per frame, by default it is private so-
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance))
        {
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("-");
        }

    }

    private void HandleMovement()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        // we were just blindly moving, so now we check if, we are colliding with anything
        // and we use RAYCASTTTTTT

        //float playerSize = .7f;
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // ATTEMPTING COLLIDE AND SLIDE 

        if (!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move only X dirn
                moveDir = moveDirX;
            }
            else
            {
                // cannot move only on the X

                // so Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove) { moveDir = moveDirZ; }
                else
                {
                    // cannot move in any direction
                }
            }
        }



        if (canMove)
        {

            // BUT RAYCAST, IS A LASER THIN LINE THAT SHOOTS, SO IF YOUR CENTRE LINE IS JUST A LIL OFF, YOU CAN PHASE THROUGH AGAIN, woof
            // SO WE USE A CAPULE CAST (capsule, is a tablet, a stretched sphere)


            transform.position += moveDir * moveDistance;

        }
        //We multiply with Time.deltaTime, because otherwise with faster framerates, youd do math quickly and thus the speed increases wayy too much
        // and thus to minimize this, we normalize it with Time.deltaTime

        // For Character Rotation
        // transform.rotation -> this however works with quaternion, which is complicated
        // transform.eulerAngles -> regular euler angles, just the angles that go from 0, 360
        // transform.lookAt -> since you already have the moveDir, you can just calculate a point in that direction and look at it
        //                      transform.lookAt -> looks at a point
        // transform.forward -> you can get and set
        // similarly there is transform.right and transform.up

        //transform.forward = -moveDir;


        // Lerp -> a mathematical function that interpolates between 2 values -> adds that smooth transition
        //         used for location
        // Slerp -> spherical interpolation
        //          used for rotation

        // however it is a little slow, so we add a rotate speed


        if (moveDir.sqrMagnitude > 0f)
        {
            isWalking = true;
            float rotateSpeed = 100f;
            transform.forward = Vector3.Slerp(transform.forward, -moveDir, Time.deltaTime * rotateSpeed);
        }
        else
        {
            isWalking = false;
        }

        // The character starts facing the screen once you stop moving because once you stop moving, youre move direction becomes (0,0,0)
        // and as a result your rotation becomes towards you, and your character faces you
        // to avoid this, we rotate only when the player is moving if (moveDir != Vector3.zero) -> if (moveDir.sqrMagnitude > 0f) #because this avoids internal calculations

    }
}

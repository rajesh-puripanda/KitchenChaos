using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    //  public float moveSpeed = 7f;
    //    However this being public, exposes this to the entire file, and you can change it from any other files unintentionally, althought having it public, 
    //    helps you change and tweak the value from the editor itself, in production ready code, that is not advisable

    //    Instead you can just use, 
    [SerializeField] private float moveSpeed = 7f;

    // Update is called once per frame, by default it is private so-
    private void Update()
    {


    Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
            float rotateSpeed = 100f;
            transform.forward = Vector3.Slerp(transform.forward, -moveDir, Time.deltaTime * rotateSpeed);
        }

        // The character starts facing the screen once you stop moving because once you stop moving, youre move direction becomes (0,0,0)
        // and as a result your rotation becomes towards you, and your character faces you
        // to avoid this, we rotate only when the player is moving if (moveDir != Vector3.zero) -> if (moveDir.sqrMagnitude > 0f) #because this avoids internal calculations
    }
}

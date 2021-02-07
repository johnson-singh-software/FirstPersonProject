using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CapsuleController : MonoBehaviour
{
    public CharacterController CapsuleControllerObjectToTransform; //this class instance is then set to the Character Controller Script on the front end which was created under Player
    public Transform ObjectToApplyCamTransform; //cameraTransform field is now available on front end, we can assign a camera into this field so this Player Controller script can use it
    public Transform groundCheckPoint; //cameraTransform field is now available on front end, we can assign a camera into this field so this Player Controller script can use it
    public GameObject raycastObject, bodyTriggerObject;

    public float moveSpeed = 8, sprintSpeed = 11, mouseSensitivity = 3, gravityModifier = 4, jumpForce = 10;
    public bool invertY;

    enum MovementStatus
    {
        normal,
        scale
    }

    private MovementStatus status = MovementStatus.normal;
    private Vector3 transformValuesUsingInput;
    private bool canJump, doubleJump;
    private Vector3 pointDir;

    void Update()
    {
        checkCollision();

        switch (status)
        {
            case MovementStatus.normal:

                normalMove();
                normalCam();
                break;
            case MovementStatus.scale:
                scaleMove();
                scaleCam();
                break;
        }
    }

    private void checkCollision()
    {
        int upperLayer = UpperCollisionScript.upperLayer;
        int lowerLayer = LowerCollisionScript.lowerLayer;

        if (upperLayer == 9)
        {
            status = MovementStatus.scale;
        }
        else // if (lowerLayer == 8)
        {
            status = MovementStatus.normal;
        }
    }

    private Vector3 usePlaneInput()
    {
        Vector3 moveForward = transform.forward * Input.GetAxisRaw("Vertical"); //transform.forward essentially returns direction
        Vector3 strafe = transform.right * Input.GetAxisRaw("Horizontal");
        transformValuesUsingInput = moveForward + strafe;
        transformValuesUsingInput.Normalize(); //dealing with diagonal movement being faster

        return transformValuesUsingInput;
    }

//    private Vector3 useScaleInput()
//    {
//        pointDir = raycastObject.transform.TransformDirection(Vector3.forward);
//        Vector3 rayStart = transform.position;
//        RaycastHit hit1, hit0, hit2;
//
//        Debug.DrawRay(rayStart, pointDir, Color.yellow);
//        Ray ray1 = new Ray(rayStart, pointDir);
//
//        Quaternion backLeftRayAngle = Quaternion.AngleAxis(-155, new Vector3(0, 1, 0));
//        Vector3 leftBackRayPosition = backLeftRayAngle * pointDir;
//        Ray ray0 = new Ray(rayStart, leftBackRayPosition);
//        Debug.DrawRay(rayStart, leftBackRayPosition, Color.blue);
//
//        Quaternion backRightRayAngle = Quaternion.AngleAxis(+155, new Vector3(0, 1, 0));
//        Vector3 rightBackRayPosition = backRightRayAngle * pointDir;
//        Ray ray2 = new Ray(rayStart, rightBackRayPosition);
//        Debug.DrawRay(rayStart, rightBackRayPosition, Color.green);
//        float reverse = 1f;
//        if (Physics.Raycast(ray1, out hit1, 4f) && hit1.collider.gameObject.layer == 9)
//        {
//            Debug.DrawRay(hit1.point, hit1.normal, Color.red);
//
//            Quaternion leftRayAngle = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
//            Vector3 leftRayPosition = leftRayAngle * hit1.normal;
//            Debug.DrawRay(hit1.point, leftRayPosition, Color.blue);
//
//            Quaternion rightRayAngle = Quaternion.AngleAxis(+90, new Vector3(0, 1, 0));
//            Vector3 rightRayPosition = rightRayAngle * hit1.normal;
//            Debug.DrawRay(hit1.point, rightRayPosition, Color.green);
//
//            rayStart = leftRayPosition;
//        }
//        else if (Physics.Raycast(ray0, out hit0, 4f) && hit0.collider.gameObject.layer == 9)
//        {
//            Debug.DrawRay(hit0.point, hit0.normal, Color.red);
//
//            Quaternion leftRayAngle = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
//            Vector3 leftRayPosition = leftRayAngle * hit0.normal;
//            Debug.DrawRay(hit0.point, leftRayPosition, Color.blue);
//
//            Quaternion rightRayAngle = Quaternion.AngleAxis(+90, new Vector3(0, 1, 0));
//            Vector3 rightRayPosition = rightRayAngle * hit0.normal;
//            Debug.DrawRay(hit0.point, rightRayPosition, Color.green);
//
//            reverse = -1f;
//            rayStart = leftRayPosition;
//        }
//        else if (Physics.Raycast(ray2, out hit2, 4f) && hit2.collider.gameObject.layer == 9)
//        {
//            Debug.DrawRay(hit2.point, hit2.normal, Color.red);
//
//            Quaternion leftRayAngle = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
//            Vector3 leftRayPosition = leftRayAngle * hit2.normal;
//            Debug.DrawRay(hit2.point, leftRayPosition, Color.blue);
//
//            Quaternion rightRayAngle = Quaternion.AngleAxis(+90, new Vector3(0, 1, 0));
//            Vector3 rightRayPosition = rightRayAngle * hit2.normal;
//            Debug.DrawRay(hit2.point, rightRayPosition, Color.green);
//
//            reverse = -1f;
//            rayStart = leftRayPosition;
//        }
//        
//        //use second collider. if second collider == 9 and first collider !=9 then stop positive vertical transform.
//        //in up thrust - set .normal, up thrust, set .normal, 
//
//        Vector3 moveUp = transform.up * Input.GetAxisRaw("Vertical") * reverse; //transform.forward essentially returns direction
//        Vector3 strafe = rayStart * Input.GetAxisRaw("Horizontal") * reverse;
//
//        transformValuesUsingInput = moveUp + strafe; //+ moveUp
//        transformValuesUsingInput.Normalize(); //dealing with diagonal movement being faster
//        transformValuesUsingInput *= 4f;
//
//        return transformValuesUsingInput;
//    }    

    private Vector3 useScaleInput()
    {
        pointDir = raycastObject.transform.TransformDirection(Vector3.forward);
        Vector3 rayStart = transform.position;
        Vector3 temp = transform.position;
        RaycastHit hit1;

        Quaternion downRayAngle = Quaternion.AngleAxis(50, new Vector3(0, 0, 1));
        Vector3 downRayDirection = downRayAngle * pointDir;

        Debug.DrawRay(rayStart, pointDir, Color.yellow);
        Debug.DrawRay(rayStart, downRayDirection, Color.yellow);

        //layer bit shift mask to collide only against layer 9
        int layerMask = 1 << 9;

        if (Physics.Raycast(rayStart, pointDir, out hit1, 4f, layerMask) ||
            Physics.Raycast(rayStart, downRayDirection, out hit1, 1.2f, layerMask))
        {
            Debug.Log("layermask hit");
            Debug.DrawRay(hit1.point, hit1.normal, Color.red);

            Quaternion leftRayAngle = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
            Vector3 leftRayPosition = leftRayAngle * hit1.normal;
            Debug.DrawRay(hit1.point, leftRayPosition, Color.blue);

            Quaternion rightRayAngle = Quaternion.AngleAxis(+90, new Vector3(0, 1, 0));
            Vector3 rightRayPosition = rightRayAngle * hit1.normal;
            Debug.DrawRay(hit1.point, rightRayPosition, Color.green);

            rayStart = leftRayPosition;

            //could add use of second collider. if second collider == 9 and first collider !=9 then stop positive vertical transform.

            Vector3 moveUp;
            if (overlapGrounded() && Input.GetAxisRaw("Vertical") < 0) //if input is down and grounded, transform backwards
            {
                moveUp = transform.forward * Input.GetAxisRaw("Vertical");
            }
            else
            {
                moveUp = transform.up * Input.GetAxisRaw("Vertical"); //otherwise transform vertically
            }

            Vector3 strafe = rayStart * Input.GetAxisRaw("Horizontal");

            transformValuesUsingInput = moveUp + strafe; //+ moveUp
            transformValuesUsingInput.Normalize(); //dealing with diagonal movement being faster
            transformValuesUsingInput *= 4f;
        }
        else
        {
            return Vector3.zero;
        }

        return transformValuesUsingInput;
    }

    private void normalMove()
    {
        applyGravity();
        run();
        jump();

        CapsuleControllerObjectToTransform.Move(transformValuesUsingInput * Time.deltaTime); //applying movement to character controller
    }

    private void scaleMove()
    {
        transformValuesUsingInput = useScaleInput();
//        Debug.Log(transform.rotation);
//        Debug.Log(UpperCollisionScript.otherRotation);
        jump();

        CapsuleControllerObjectToTransform.Move(transformValuesUsingInput * Time.deltaTime); //applying movement to character controller
    }

    private void applyGravity()
    {
        float yStore = transformValuesUsingInput.y; //get current y position
        transformValuesUsingInput = usePlaneInput(); //will change y position
        transformValuesUsingInput.y = yStore; //reset to current y position
        if (CapsuleControllerObjectToTransform.isGrounded) //if grounded, keep y constant
        {
            transformValuesUsingInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        else //not grounded, keep y increasing until grounded
        {
            transformValuesUsingInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
    }

    private void run()
    {
        // *= Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") == 1 ? sprintSpeed : moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && canJump)
        {
            transformValuesUsingInput.x *= sprintSpeed;
            transformValuesUsingInput.z *= sprintSpeed;
        }
        else
        {
            transformValuesUsingInput.x *= moveSpeed;
            transformValuesUsingInput.z *= moveSpeed;
        }
    }

    private readonly Vector3 groundBoxSize = new Vector3(0.475f, 0.075f, 0.475f);

    private void jump()
    {
        //canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, checkLayer).Length > 0; //method returns array of objects in sphere

        if (status == MovementStatus.normal)
        {
            canJump = overlapGrounded();
        }
        else
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transformValuesUsingInput.y = jumpForce;
            canJump = false; //once jumped, set to false
            doubleJump = true; //enable double jump
            status = MovementStatus.normal; //whenever a player jumps, return movement to normal (gravity, directional movement) e.g. on ladders etc
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump) //will register as doubleJump is true into the next Update() frames?
        {
            transformValuesUsingInput.y = jumpForce * 1.1f; //multiplied the jumpForce by 1.1f to add 10% extra force because it feels more natural in game
            doubleJump = false; //after double jump, disable double jump
        }
    }

    private Boolean overlapGrounded()
    {
        Vector3 groundCheckOrigin = groundCheckPoint.position;
        return Physics.OverlapBox(groundCheckOrigin, groundBoxSize).Length > 0; //method returns array of objects in sphere. greater than 1 because character collider will always be inside boxcast
    }

    void OnDrawGizmos()
    {
//        Gizmos.color = Color.blue;
//        Gizmos.DrawSphere(groundCheckPoint.position, 0.25f);
        Vector3 pos = CapsuleControllerObjectToTransform.transform.position;
        pos.y -= 1.01f;

//        Gizmos.color = Color.white;
//        Gizmos.DrawWireSphere(pos, 0.25f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(pos, groundBoxSize * 2);
    }

    private Vector2 getRawMouseInput()
    {
        //control camera rotation, x and y axis stored as Vector2 data type
        //get raw mouse input (no smoothing)
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        mouseInput.y = invertY ? -mouseInput.y : mouseInput.y; //invert Y check

        return mouseInput;
    }

    private void normalCam()
    {
        Vector2 mouseInput = getRawMouseInput();
        rotateCapsuleHorizontal(mouseInput.x);
        rotateCameraVertical(mouseInput.y);
    }

    private void scaleCam()
    {
        Vector2 mouseInput = getRawMouseInput();

        rotateCapsuleHorizontal(mouseInput.x);
        rotateCameraVertical(mouseInput.y);
    }

    private float xAxisValue;

    private void rotateCapsuleHorizontal(float mouseInputX)
    {
        xAxisValue += mouseInputX;
        //Debug.Log("1: " + xAxisValue);
        xAxisValue %= 360;
        //Debug.Log("2: " + xAxisValue);
        xAxisValue = Math.Abs(xAxisValue);
        //Debug.Log("3: " + xAxisValue);
        //horizontal camera rotation. this transform.rotation is used on the camera follow point
        Quaternion rotation = transform.rotation; //get current rotation
        //Quarternion deals with complex x.y.z and w values. Euler essentially converts that into usable Vector3 x,y,z 
        //if you look at the player capsule mesh, the y axis actually rotates horizontally. we want this to rotate using the mouse input x axis value.
        //this is why the y axis euler parameter is fed the x mouse input

        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + mouseInputX, rotation.eulerAngles.z);
        transform.rotation = rotation; //set new rotation
    }

    private float yAxisValue;

    private void rotateCameraVertical(float mouseInputY)
    {
        yAxisValue += mouseInputY;

        if (yAxisValue > 90.0f)
        {
            yAxisValue = 90.0f;
            mouseInputY = 0.0f;
            clampYAxisRotationToValue(270.0f);
        }
        else if (yAxisValue < -90.0f)
        {
            yAxisValue = -90.0f;
            mouseInputY = 0.0f;
            clampYAxisRotationToValue(90.0f);
        }

        ObjectToApplyCamTransform.transform.Rotate(Vector3.left * mouseInputY);
    }

    private void clampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = ObjectToApplyCamTransform.transform.eulerAngles;
        eulerRotation.x = value;
        ObjectToApplyCamTransform.transform.eulerAngles = eulerRotation;
    }

    private void rotateCameraHorizontal(float mouseInputX)
    {
        ObjectToApplyCamTransform.rotation =
            Quaternion.Euler(ObjectToApplyCamTransform.rotation.eulerAngles +
                             new Vector3(0f, mouseInputX,
                                 0f));
    }
}
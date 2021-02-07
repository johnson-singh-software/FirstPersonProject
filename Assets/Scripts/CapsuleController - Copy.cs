//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Serialization;
//
//public class CapsuleController : MonoBehaviour
//{
//    public CharacterController CapsuleControllerObjectToTransform; //this class instance is then set to the Character Controller Script on the front end which was created under Player
//    public Transform ObjectToApplyCamTransform; //cameraTransform field is now available on front end, we can assign a camera into this field so this Player Controller script can use it
//    public Transform groundCheckPoint; //cameraTransform field is now available on front end, we can assign a camera into this field so this Player Controller script can use it
//    public GameObject raycastObject, bodyTriggerObject;
//
//    enum MovementStatus
//    {
//        normal,
//        scale
//    }
//
//    private MovementStatus status = MovementStatus.normal;
//
//    public float moveSpeed = 8, sprintSpeed = 11, mouseSensitivity = 3, gravityModifier = 4, jumpForce = 10;
//    public bool invertY;
//
//    private Vector3 transformValuesUsingInput;
//    private bool physInteract = false, canJump, doubleJump;
//
//    void Update()
//    {
//        switch (status)
//        {
//            case MovementStatus.normal:
//                normalMove();
//                normalCam();
//                break;
//            case MovementStatus.scale:
//                scaleMove();
//                scaleCam();
//                break;
//        }
//    }
//    
//    private void OnControllerColliderHit(ControllerColliderHit other)
//    {
//        int layerID = other.gameObject.layer;
//        checkScaleObject(layerID);
//    }
//
//    void FixedUpdate()
//    {
//        checkPhysInteract();
//    }
//
//    private Vector3 pointDir;
//    public float maxRayDistance;
//
////    private void rayCastAhead()
////    {
////        pointDir = raycastObject.transform.TransformDirection(Vector3.forward);
////        Vector3 rayStart = raycastObject.transform.position;
////        RaycastHit hit1;
////
////        Debug.DrawRay(rayStart, pointDir, Color.yellow);
////        Ray ray1 = new Ray(rayStart, pointDir);
////
////        Quaternion leftRayAngle = Quaternion.AngleAxis(-89, new Vector3(0, 1, 0));
////        Vector3 leftRayPosition = leftRayAngle * pointDir;
////
////        Quaternion rightRayAngle = Quaternion.AngleAxis(+89, new Vector3(0, 1, 0));
////        Vector3 rightRayPosition = rightRayAngle * pointDir;
////
////        Ray ray0 = new Ray(rayStart, leftRayPosition);
////        Ray ray2 = new Ray(rayStart, rightRayPosition);
////
////        RaycastHit hit0;
////        RaycastHit hit2;
////        Debug.DrawRay(rayStart, leftRayPosition, Color.red);
////        Debug.DrawRay(rayStart, rightRayPosition, Color.white);
////
////        if (Physics.Raycast(ray1, out hit1, maxRayDistance, ladderLayer) && hit1.distance <= 0.7f ||
////            Physics.Raycast(ray0, out hit0, maxRayDistance, ladderLayer) && hit0.distance <= 0.5f ||
////            Physics.Raycast(ray2, out hit2, maxRayDistance, ladderLayer) && hit2.distance <= 0.7f)
////        {
////                status = MovementStatus.scale;
////                resetTransformation();
////        }
////        else
////        {
////            status = MovementStatus.normal;
////        }
////    }
//
//    private void checkPhysInteract()
//    {
//        //https://www.youtube.com/watch?v=6C4KfuW2q8Y
//        //setup empty object for bodyTriggerObject, link it into here
//        //make child of ladder
//        //add box collider, check is trigger
//        
////        //start script
////        private void OnTriggerEnter(Collider other)
////        {
////
////            if (other.tag or layer == scale)
////            {
////                status = MovementStatus.scale;
////            }
////        }
////        private void OnTriggerExit(Collider other)
////        {
////
////            if (other.tag or layer == scale)
////            {
////                status = MovementStatus.normal;
////            }
////
////            onCollisionEnter using GameObject.layer?
////                //https://answers.unity.com/questions/1382055/how-do-you-trigger-a-specific-collider-using-ontri.html
////        }
//
//    }
//
//    private void checkScaleObject(int layerID)
//    {
//        Debug.Log("collide status method called" + layerID);
//        switch (layerID)
//        {
//            case 9: //scale
//                status = MovementStatus.scale;
//                Debug.Log("scale status" + layerID);
//                break;
//            case 8: //plane
//                status = MovementStatus.normal;
//                Debug.Log("grounded status" + layerID);
//                break;
//        }
//    }
//
//    private Vector3 usePlaneInput()
//    {
//        Vector3 moveForward = transform.forward * Input.GetAxisRaw("Vertical"); //transform.forward essentially returns direction
//        Vector3 strafe = transform.right * Input.GetAxisRaw("Horizontal");
//        transformValuesUsingInput = moveForward + strafe;
//        transformValuesUsingInput.Normalize(); //dealing with diagonal movement being faster
//
//        return transformValuesUsingInput;
//    }
//
//    private Vector3 useScaleInput()
//    {
//        Vector3 moveUp = transform.up * Input.GetAxisRaw("Vertical"); //transform.forward essentially returns direction
//        Vector3 strafe = transform.right * Input.GetAxisRaw("Horizontal");
//        transformValuesUsingInput = moveUp + strafe;
//        transformValuesUsingInput.Normalize(); //dealing with diagonal movement being faster
//        transformValuesUsingInput *= 4f;
//
//        return transformValuesUsingInput;
//    }
//
//    private void resetTransformation()
//    {
//        transformValuesUsingInput = CapsuleControllerObjectToTransform.transform.position;
//    }
//
//    private void normalMove()
//    {
//        applyGravity();
//        run();
//        jump();
//
//        CapsuleControllerObjectToTransform.Move(transformValuesUsingInput * Time.deltaTime); //applying movement to character controller
//    }
//
//    private void scaleMove()
//    {
//        transformValuesUsingInput = useScaleInput();
//        jump();
//
//        CapsuleControllerObjectToTransform.Move(transformValuesUsingInput * Time.deltaTime); //applying movement to character controller
//    }
//
//    private void applyGravity()
//    {
//        float yStore = transformValuesUsingInput.y; //get current y position
//        transformValuesUsingInput = usePlaneInput(); //will change y position
//        transformValuesUsingInput.y = yStore; //reset to current y position
//        if (CapsuleControllerObjectToTransform.isGrounded) //if grounded, keep y constant
//        {
//            transformValuesUsingInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
//        }
//        else //not grounded, keep y increasing until grounded
//        {
//            transformValuesUsingInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
//        }
//    }
//
//    private void run()
//    {
//        // *= Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") == 1 ? sprintSpeed : moveSpeed;
//
//        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && canJump)
//        {
//            transformValuesUsingInput.x *= sprintSpeed;
//            transformValuesUsingInput.z *= sprintSpeed;
//        }
//        else
//        {
//            transformValuesUsingInput.x *= moveSpeed;
//            transformValuesUsingInput.z *= moveSpeed;
//        }
//    }
//
//    private readonly Vector3 groundBoxSize = new Vector3(0.475f, 0.075f, 0.475f);
//
//    private void jump()
//    {
//        //canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, checkLayer).Length > 0; //method returns array of objects in sphere
//
//
//        if (status == MovementStatus.normal)
//        {
//            canJump = overlapGrounded();
//        }
//        else
//        {
//            canJump = true;
//        }
//
//        if (Input.GetKeyDown(KeyCode.Space) && canJump)
//        {
//            transformValuesUsingInput.y = jumpForce;
//            canJump = false; //once jumped, set to false
//            doubleJump = true; //enable double jump
//            status = MovementStatus.normal; //whenever a player jumps, return movement to normal (gravity, directional movement) e.g. on ladders etc
//        }
//        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump) //will register as doubleJump is true into the next Update() frames?
//        {
//            transformValuesUsingInput.y = jumpForce * 1.1f; //multiplied the jumpForce by 1.1f to add 10% extra force because it feels more natural in game
//            doubleJump = false; //after double jump, disable double jump
//        }
//    }
//
//    private Boolean overlapGrounded()
//    {
//        Vector3 groundCheckOrigin = groundCheckPoint.position;
//        return Physics.OverlapBox(groundCheckOrigin, groundBoxSize).Length > 1; //method returns array of objects in sphere. greater than 1 because character collider will always be inside boxcast
//    }
//
//    void OnDrawGizmos()
//    {
////        Gizmos.color = Color.blue;
////        Gizmos.DrawSphere(groundCheckPoint.position, 0.25f);
//        Vector3 pos = CapsuleControllerObjectToTransform.transform.position;
//        pos.y -= 1.01f;
//
////        Gizmos.color = Color.white;
////        Gizmos.DrawWireSphere(pos, 0.25f);
//
//        Gizmos.color = Color.cyan;
//        Gizmos.DrawWireCube(pos, groundBoxSize * 2);
//    }
//
//    private Vector2 getRawMouseInput()
//    {
//        //control camera rotation, x and y axis stored as Vector2 data type
//        //get raw mouse input (no smoothing)
//        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
//
//        mouseInput.y = invertY ? mouseInput.y : -mouseInput.y; //invert Y check
//
//        return mouseInput;
//    }
//
//    private void normalCam()
//    {
//        Vector2 mouseInput = getRawMouseInput();
//        rotateCapsuleHorizontal(mouseInput.x);
//        rotateCameraVertical(mouseInput.y);
//    }
//
//    private void scaleCam()
//    {
//        Vector2 mouseInput = getRawMouseInput();
//
//        rotateCameraHorizontal(mouseInput.x);
//        rotateCameraVertical(mouseInput.y);
//    }
//
//    private void rotateCapsuleHorizontal(float mouseInputX)
//    {
//        //horizontal camera rotation. this transform.rotation is used on the camera follow point
//        Quaternion rotation = transform.rotation; //get current rotation
//        //Quarternion deals with complex x.y.z and w values. Euler essentially converts that into usable Vector3 x,y,z 
//        //if you look at the player capsule mesh, the y axis actually rotates horizontally. we want this to rotate using the mouse input x axis value.
//        //this is why the y axis euler parameter is fed the x mouse input
//        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + mouseInputX, rotation.eulerAngles.z);
//        transform.rotation = rotation; //set new rotation
//    }
//
//    private void rotateCameraVertical(float mouseInputY)
//    {
//        //vertical camera rotation, this rotation is only on the camera as we assigned Main Camera to this Player Controller Script in the front end
//
//        //mouse Y input is clamped and "inverted"
//        float clampedYInput = Mathf.Clamp(mouseInputY, -90f, 90f); //this doesnt seem to affect anything.
//
//        ObjectToApplyCamTransform.rotation =
//            Quaternion.Euler(ObjectToApplyCamTransform.rotation.eulerAngles +
//                             new Vector3(clampedYInput, 0f,
//                                 0f));
//
//        //feeding it a vector3 by adding
//
//        //ObjectToApplyCameraTransform.rotation = Quaternion.AngleAxis(clampedYInput, Vector3.right);
//    }
//    
//    private void rotateCameraHorizontal(float mouseInputX)
//    {
//        ObjectToApplyCamTransform.rotation =
//            Quaternion.Euler(ObjectToApplyCamTransform.rotation.eulerAngles +
//                             new Vector3(0f, mouseInputX,
//                                 0f));
//    }
//    
//    private void alignCapsuleToCamFollowPoint(float mouseInputX)
//    {
//        transform.rotation = ObjectToApplyCamTransform.rotation;
//    }
//}
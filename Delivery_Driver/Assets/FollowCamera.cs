using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // this "things" positon should be the same as the camera's position in game
    [SerializeField] GameObject thingToFollow;

    // put in late update to ensure that the camera follows the car 
    // its because of the unity execution order. Without this, the camera may be jittery.
    void LateUpdate()
    {
        // if you dont add the (0,0,-10) to the position of the camera, you will not be able to see the scene
        // because you are matching the view's car directly, so of course you won't be able to see the car
        // you want the camera to be slightly back so that u can view the entire scene
        transform.position = thingToFollow.transform.position + new Vector3(0,0,-10);
    }
}

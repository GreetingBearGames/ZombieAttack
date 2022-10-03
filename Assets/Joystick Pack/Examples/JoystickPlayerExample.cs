using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;

    public void FixedUpdate()
    {
        
    }
    public void Move(){
        Vector2 direction = Vector2.up * fixedJoystick.Vertical + Vector2.right * fixedJoystick.Horizontal;
        transform.Translate(direction* speed* Time.deltaTime, Space.World);
    }
}
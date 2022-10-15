using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public void FixedUpdate()
    {
        
    }
    public void Move(float speed){
        Vector2 direction = Vector2.up * fixedJoystick.Vertical + Vector2.right * fixedJoystick.Horizontal;
        transform.Translate(direction * speed, Space.World);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : IPlayerInputs
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
    public bool Run => Input.GetButton("Run");
    public bool Jump => Input.GetButton("Jump");
    public float LookVertical => Input.GetAxis("Mouse Y");
    public float LookHorizontal => Input.GetAxis("Mouse X");
}

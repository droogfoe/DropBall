using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern.Move;

[RequireComponent(typeof(MovementCommander))]
public class CharactoerController : MonoBehaviour
{
    private MovementCommander movementCommander;
    private AxisMoveCommand joystickInputCommand;


    // Start is called before the first frame update
    void Start()
    {
        movementCommander = GetComponent<MovementCommander>();
        joystickInputCommand = new AxisMoveCommand(movementCommander);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hSpd = Input.GetAxis("Horizontal");
        float vSpd = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(hSpd, 0, vSpd);
        if (Mathf.Abs(hSpd) >  0 || Mathf.Abs(vSpd) > 0)
        {
            joystickInputCommand.Execute(dir);
        }
    }
}

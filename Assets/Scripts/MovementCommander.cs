using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern.Move;

[RequireComponent(typeof(Rigidbody))]

public class MovementCommander : MonoBehaviour
{
    public Transform Rotator;
    public float moveSpd = 1.5f;
    private float movegapValue = 0.1f;
    private Rigidbody rb;
    private ProceduralFeedBack pFeedBack;

    private void Start()
    {
        rb        = GetComponent<Rigidbody>();
        Rotator   = transform.GetChild(0);
        pFeedBack = Rotator.gameObject.GetComponent<ProceduralFeedBack>();
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public void AxisMoveExecute(object _dirValue)
    {
        Vector3 _currentDirValue = (Vector3)_dirValue;
        Move(_currentDirValue);
    }

    private void Move(Vector3 _dirValue)
    {
        Vector3 targetValue = new Vector3(
            _dirValue.x * moveSpd, 
            rb.velocity.y, 
            _dirValue.z * moveSpd);
        rb.velocity = targetValue;

        //Rotate(rb.velocity);

        if (_dirValue != Vector3.zero)
        {
            pFeedBack.pannerTimer = 0;
        }
        pFeedBack.velocity = rb.velocity;
    }
    private void Rotate(Vector3 _dirValue)
    {
        Vector3 dir = _dirValue.normalized;
        Rotator.RotateAround(Vector3.Cross(dir, Vector3.down), _dirValue.sqrMagnitude * 0.01f);
    }
}

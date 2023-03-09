using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    void Start ()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactabe = hit.collider.GetComponent<Interactable>();
                if (interactabe != null)
                {
                    SetFocus(interactabe);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RemoveFocus();
        }
    }

    void SetFocus(Interactable newFocus)
    {

        if(newFocus != focus)
        {
            if (focus != null)
            {
            focus.OnDefocused();
            }
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
        focus.OnDefocused();
        }
        focus = null;
    }

}

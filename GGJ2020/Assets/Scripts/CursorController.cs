using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    [SerializeField] Texture2D selectIcon;
    LevelController lc;

    GameObject movingObject;
    Vector3 moveVec;

    LayerMask moveableMask;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(selectIcon, Vector2.zero, CursorMode.Auto);
        moveableMask = LayerMask.GetMask("Moveable");
        lc = LevelController.instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (lc.input.Click.WasPressed)
        {

            SelectNewObject();

        }

        if(movingObject != null)
        {
            if (lc.input.Click.WasReleased)
            {
                DropObject();
            }


        }

    }

    /// <summary>
    /// Player has Clicked on an object
    /// </summary>
    void SelectNewObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,moveableMask);
        if(hit.collider != null)
        {
            movingObject = hit.collider.gameObject;
            ToggleInteractableEngaged(true);
        }

    }

    /// <summary>
    /// Player has released object
    /// </summary>
    void DropObject()
    {
        ToggleInteractableEngaged(false);
        movingObject = null;
    }

    /// <summary>
    /// Helper method for toggling engagement with facemouse component
    /// </summary>
    /// <param name="tf"></param>
    void ToggleInteractableEngaged(bool tf) {

        if (movingObject.GetComponent<Interactable>())
        {
            movingObject.GetComponent<Interactable>().engaged = tf;
        }
        else
        {
            Debug.LogWarning("No Interactable component on object that is moveable. D:");
        }
    }

}

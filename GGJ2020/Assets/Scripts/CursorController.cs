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

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(selectIcon, Vector2.zero, CursorMode.Auto);
        moveableMask = LayerMask.GetMask("Moveable");
        lc = LevelController.instance;
        cam = Camera.main;
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
            else
            {
                moveVec = cam.ScreenToWorldPoint(Input.mousePosition);
                moveVec.z = 0;
                movingObject.transform.position = moveVec;
            }

        }



    }


    void SelectNewObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,moveableMask);
        if(hit.collider != null)
        {
            movingObject = hit.collider.gameObject;
        }

    }

    void DropObject()
    {
        movingObject = null;
    }

   


}

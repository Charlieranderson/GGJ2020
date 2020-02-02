using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    Camera cam;
    public bool playerHasControl;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    public bool hitThisFrame;
    RaycastHit cursorHit;
    public GameObject reticle;

    public DOTweenAnimation shakeAnim;

    public GameObject hoveredObject;
    public GameObject heldObject;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();

        //reticle = Instantiate(reticle);
    }

    void OnMouseDown()
    {
        //Vector2 mousePos = Input.mousePosition;
        //float distance = mainCam.WorldToScreenPoint(t.position).z;
        //Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
        //offset = t.position - worldPos;

    }

    IEnumerator ClickRoutine()
    {

        yield return new WaitForSeconds(.5f);

    }

    void OnMouseDrag()
    {
        //Vector2 mousePos = Input.mousePosition;
        //float distance = mainCam.WorldToScreenPoint(t.position).z;
        //t.position = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)) + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            reticle.transform.DOScale(.4f, .05f).SetEase(Ease.OutQuad);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            reticle.transform.DOScale(.3f, .05f).SetEase(Ease.OutQuad);
        }

        //reticle.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);

        //Player Cursor Logic
        Ray ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));

        //if we hit something with the ray
        if (Physics.Raycast(ray, out cursorHit, 1000))
        {
            reticle.transform.position = cursorHit.point + new Vector3(0, 0, -.5f);
            hoveredObject = cursorHit.transform.gameObject;

            hitThisFrame = true;

            if (cursorHit.transform.gameObject.tag == "Draggable")
            {
                print("got a valid object");


                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    print("got mouse down");
                    if (hoveredObject != null)
                    {
                        heldObject = hoveredObject;
                        //heldObject.transform.GetChild(1).GetComponent<DOTweenAnimation>().DOTogglePause();
                        //heldObject.transform.GetChild(0).GetComponent<TextMesh>().text = "";

                        if (heldObject.GetComponent<FaceMouse>())
                        {
                            heldObject.GetComponent<FaceMouse>().engaged = true;
                        }
                    }
                }
            }

            if (cursorHit.transform.gameObject.GetComponent<DragTarget>())
            {
                print("got a valid object");


                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hoveredObject != null)
                    {
                        heldObject = hoveredObject;
                        heldObject.transform.GetChild(1).GetComponent<DOTweenAnimation>().DOTogglePause();
                        heldObject.transform.GetChild(0).GetComponent<TextMesh>().text = "";
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (heldObject != null)
                {
                    //heldObject.transform.GetChild(1).GetComponent<DOTweenAnimation>().DOTogglePause();
                    //heldObject.transform.GetChild(0).GetComponent<TextMesh>().text = "TAKE ME BACK";

                    if (heldObject.GetComponent<FaceMouse>())
                    {
                        heldObject.GetComponent<FaceMouse>().engaged = false;
                    }

                    heldObject = null;

                }
            }
        }
        else
        {
            hitThisFrame = false;


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (heldObject != null)
                {
                    //heldObject.transform.GetChild(1).GetComponent<DOTweenAnimation>().DOTogglePause();
                    //heldObject.transform.GetChild(0).GetComponent<TextMesh>().text = "TAKE ME BACK";

                    if (heldObject.GetComponent<FaceMouse>())
                    {
                        heldObject.GetComponent<FaceMouse>().engaged = false;
                    }

                    heldObject = null;

                }
            }

            hoveredObject = null;
        }

        //Player interaction with the level
        if (playerHasControl)
        {
            //DragCamera(); //Player Camera Movement
            //Cam Movement
            //if (Input.GetMouseButtonDown(0))//when the player clicks, assign the drag origin
            //{
            //    dragOrigin = Input.mousePosition;
            //    return;
            //}

            //if (!Input.GetMouseButton(0)) return;//if the mouse isn't held, return

            // if (dragOrigin != Input.mousePosition) { //if its held and different
            //     Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            //     cam.transform.Translate(-pos, Space.World);
            //     dragOrigin = Input.mousePosition;
            // }

            //camera zoom
            if (Input.GetKeyDown(KeyCode.U))
            {
                cam.orthographicSize += 1;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                cam.orthographicSize -= 1;
            }

            if (hitThisFrame)
            {
                PlayerInteraction();
            }
        }

    }

    void PlayerInteraction()
    {/*
        POI hitPOI = cursorHit.transform.GetComponent<POI>();

        //if we aren't over a poi / over a hidden poi, just show the defaults and return
        if (hitPOI == null || hitPOI.hiddenFromPlayer)
        {
            objectIdentifyProgress = 0;
            return;
        }

        //if we're hovering over a point of interest
        if (hitPOI)
        {
            //if it is ground
            if (hitPOI.GetComponent<MovableArea>())
            {
                print("found a movable area");
                MovableArea hitArea = hitPOI.GetComponent<MovableArea>();

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    currentSquadSelection.GetToPoint(cursorHit.point);
                }
            }

            //if its a door
            else if (hitPOI.GetComponent<Door>())
            {
                print("it has a door");
                Door hitDoor = hitPOI.GetComponent<Door>();

                if (hitDoor.locked)
                {
                    print("it was locked");
                    if (hitDoor.signalLocked)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            print("so we tried to  unlock it");
                            hitDoor.locked = false;
                        }
                    }
                }
            }

            else if (hitPOI.GetComponent<ElectronicSwitch>())
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    ElectronicSwitch hitSwitch = hitPOI.GetComponent<ElectronicSwitch>();

                    hitSwitch.on = !hitSwitch.on;
                }
            }

            else if (hitPOI.GetComponent<CoverArea>())
            {
                print("it has cover");
                CoverArea hitCoverArea = hitPOI.GetComponent<CoverArea>();
            }

            //write out the text depending on the type of object and its status
            if (objectsIdentified.Contains(hit.transform.gameObject))
            {
                print("object is identified");

                hoverText.GetComponent<TextMeshPro>().SetText(hit.transform.name);

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (hit.transform.GetComponent<Door>())
                    {
                        print("it has a door");
                        if (hit.transform.GetComponent<Door>().locked)
                        {
                            print("it was locked");
                            if (hit.transform.GetComponent<Door>().signalLocked)
                            {
                                print("so we tried to  unlock it");
                                hit.transform.GetComponent<Door>().locked = false;
                            }
                        }
                    }
                }
            }         
        }
        */
    }

    void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        cam.transform.Translate(-move, Space.World);

        dragOrigin = Input.mousePosition;
    }

    public void ZoomTo(GameObject target)
    {
        playerHasControl = false;
        cam.transform.DOMove(new Vector3(target.transform.position.x, cam.transform.position.y, target.transform.position.z), 5).SetSpeedBased(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    public bool engaged;
    public float range;
    public bool restrictToOneSide;
    public bool restrictToRight;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (engaged)
        {
            if (restrictToOneSide)
            {
                if (restrictToRight)
                {
                    if (mousePosition.x > transform.position.x)
                    {
                        if (mousePosition.y < transform.position.y + range && mousePosition.y > transform.position.y - range)
                        {
                            Vector2 direction = new Vector2(
                                mousePosition.x - transform.position.x,
                                mousePosition.y - transform.position.y
                                );

                            transform.up = direction;
                        }
                    }
                }
                else if (!restrictToRight)
                {
                    if (restrictToRight)
                    {
                        if (mousePosition.x < transform.position.x)
                        {
                            if (mousePosition.y < transform.position.y + range && mousePosition.y > transform.position.y - range)
                            {
                                Vector2 direction = new Vector2(
                                    mousePosition.x - transform.position.x,
                                    mousePosition.y - transform.position.y
                                    );

                                transform.up = direction;
                            }
                        }
                    }
                }
            }
            else if (!restrictToOneSide) {
                if (mousePosition.y < transform.position.y + range && mousePosition.y > transform.position.y - range)
                {
                    Vector2 direction = new Vector2(
                        mousePosition.x - transform.position.x,
                        mousePosition.y - transform.position.y
                        );

                    transform.up = direction;
                }
            }
            /*
            if (mousePosition.x > transform.position.x)
            {
                if (mousePosition.y < transform.position.y + range && mousePosition.y > transform.position.y - range)
                {
                    Vector2 direction = new Vector2(
                        mousePosition.x - transform.position.x,
                        mousePosition.y - transform.position.y
                        );

                    transform.up = direction;
                }
            }
            */
        }
    }
}

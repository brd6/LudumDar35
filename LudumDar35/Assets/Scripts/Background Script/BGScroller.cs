using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BGScroller : MonoBehaviour {

    public Vector2 speed = new Vector2(2, 2);
    public Vector2 direction = new Vector2(0, -1);

    private Camera mainCamera;

    private List<Transform> backgroundPart;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    // Use this for initialization
    void Start()
    {
        backgroundPart = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Add only the visible children
            if (child.GetComponent<Renderer>() != null)
            {
                backgroundPart.Add(child);
            }
        }

        backgroundPart = backgroundPart.OrderBy(
                          t => t.position.y
                        ).ToList();
    }

    public bool ObjectIsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    // Update is called once per frame
    void Update () {
        Vector3 movement = new Vector3(
      speed.x * direction.x,
      speed.y * direction.y,
      0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        // loop
        Transform firstChild = backgroundPart.FirstOrDefault();

        if (firstChild != null)
        {
            // out the camera
            if (firstChild.position.y < Camera.main.transform.position.y)
            {
                if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                {
                    // get last child pos
                    Transform lastChild = backgroundPart.LastOrDefault();
                    Vector3 lastPosition = lastChild.transform.position;
                    Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                    // set new pos of child
                    firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);

                    // add new and remove old child
                    backgroundPart.Remove(firstChild);
                    backgroundPart.Add(firstChild);
                }
            }
        }
    }
}

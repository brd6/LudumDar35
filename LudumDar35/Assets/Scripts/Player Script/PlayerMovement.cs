using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;
    public float speed = 10f;
    public float maxVelocityX = 3f;

    [SerializeField]
    private Rigidbody2D myBody;

    private bool isAlive;
    private bool rightMove;
    private Button leftTap;
    private Button rightTap;
    private Button shapeShiftTap;

    private Vector2 startPosTouch;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

    }

    void HandlerMobileTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector2 ss = new Vector2(touch.position.x, transform.position.y);

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
                touchPosition.y = transform.position.y;
                //print("Move : " + touchPosition + " player: " + transform.position);
                transform.position = Vector3.Lerp(transform.position, touchPosition, speed);

                GetComponent<AudioSource>().Play();
            }
        }
    }

    void Update()
    {
        if (GamePlayController.instance.isGameOver == false)
        {
            HandlerMobileTouch();
        }
    }
}

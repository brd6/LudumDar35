using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float turnSpeed = 50f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Player")
        {
            if (name.Contains(target.name))
            {
                GameManager.instance.InscreasePlayerScore();
                AudioSource.PlayClipAtPoint(GamePlayController.instance.okSound, transform.position);
            }
            else
            {
                GameManager.instance.DecreasePlayerLife();
                AudioSource.PlayClipAtPoint(GamePlayController.instance.failSound, transform.position);
            }
        }
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.y <= 0f)
        {
            DestroyObject(gameObject);
        }
    }
}

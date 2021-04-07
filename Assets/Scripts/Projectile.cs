using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Color color;
    public Transform hitPartical;

    private bool collided;

	private void Start()
	{
        Destroy(gameObject, 5f);
	}

	private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Instantiate(hitPartical, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

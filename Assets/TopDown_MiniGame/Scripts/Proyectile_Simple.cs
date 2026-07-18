using UnityEngine;

public class Proyectile_Simple : MonoBehaviour {
    public enum CollisionTarget {PLAYER, ENEMIES}
    public CollisionTarget collisionTarget;
    public float lifeTime = 3.0f;
    public float speed = 1.5f;

    void Start () {
        Destroy (gameObject, lifeTime);
    }

    void Update () {
        transform.Translate(transform.forward * speed, Space.World);
    }

    void OnCollisionEnter(Collision collision){
        if (collisionTarget == CollisionTarget.PLAYER && collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerBehavior>().DamagePlayer();
        }
        else if (collisionTarget == CollisionTarget.ENEMIES && collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<NPC_Enemy>().Damage();
        }
        else if (collision.gameObject.tag == "Finish") {
            // "Finish" is used to tag level geometry — standard Unity tag,
            // kept to avoid asset-import issues with a custom tag name.
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float velocity;
    public Vector3 initialDir;
    public float distanceOffset;
    public Transform point;
    public float distanceFromPlayer;
    public MeshFilter mesh;
    public Bounds bounds;
    //Correção de tempo para colidir novamente;
    public float preventCollisionErrorsOffsetTime;
    public bool collided;
    private float counter;

    public static Ball instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        bounds = mesh.mesh.bounds;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveConstantly();
        PreventMultipleCollisions();
    }

    void MoveConstantly() {
        transform.position += initialDir.normalized * velocity * Time.fixedDeltaTime;
    }
    Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2 * Vector3.Dot(v, n) * n;
    }
    void PreventMultipleCollisions() {
        //Detecta área ocupada pela bola.
        bounds.center = Ball.instance.transform.position;

        if(collided) {
            if(counter < preventCollisionErrorsOffsetTime) {
                counter += Time.deltaTime;
                return;
            }
            else {
                collided = false;
                counter = 0;
            }
        }
    }
}

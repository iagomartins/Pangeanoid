using UnityEngine;

public class Sides : Wall
{
    public Side side;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Calcula a distância da bola durante o jogo.
        distanceFromBall = (new Vector3(transform.position.x, Ball.instance.transform.position.y, Ball.instance.transform.position.z) - Ball.instance.transform.position).magnitude;
        ReflectBall();
    }

    public override Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2 * Vector3.Dot(v, n) * n;
    }

    public override void DrawWallLine(float offsetWidth)
    {
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - offsetWidth, transform.position.z), new Vector3(transform.position.x, transform.position.y + offsetWidth, transform.position.z));
    }

    public void ReflectBall() {
        if (distanceFromBall <= 1.0f) {
            Ball.instance.initialDir = Reflect(Ball.instance.initialDir, transform.position.normalized);
        }
    }
}

public enum Side {
    LEFT,
    RIGHT
}

using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool showGizmos;
    public float offsetWidth;
    public Vector3 normal;
    public float distanceFromBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceFromBall = (new Vector3(Ball.instance.transform.position.x, transform.position.y, Ball.instance.transform.position.z) - Ball.instance.transform.position).magnitude;
        //Verifica colisões durante o jogo.
        ReflectBall();
    }

    private void OnDrawGizmos()
    {
        DrawWallLine(offsetWidth);
    }

    public virtual Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2 * Vector3.Dot(v, n) * n;
    }

    /// <summary>
    /// Desenha uma linha mostrando visualmente a área de colisão do objeto.
    /// </summary>
    /// <param name="offsetWidth">
    /// Tamanho da área de colisão.
    /// </param>
    public virtual void DrawWallLine(float offsetWidth) {
        if (showGizmos) {
            Gizmos.DrawLine(new Vector3 (transform.position.x - offsetWidth, transform.position.y, transform.position.z), new Vector3 (transform.position.x + offsetWidth, transform.position.y, transform.position.z));
        }
    }

    private void ReflectBall() {
        if (distanceFromBall <= 1.0f) {
            Ball.instance.initialDir = Reflect(Ball.instance.initialDir, transform.position.normalized);
        }
    }
}

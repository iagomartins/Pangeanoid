using UnityEngine;

public class Bar : MonoBehaviour
{
    [Header("Colisões:")]
    public MeshFilter mesh;
    public Bounds bounds;

    public Bounds playerBounds;
    public bool collided;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>();
        //Detecta a área ocupada pela barra.
        bounds = mesh.mesh.bounds;
        bounds.center = transform.position;
        bounds.Expand(new Vector3(4, 1, 1));
        Debug.Log(bounds.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(Ball.instance != null) {
            playerBounds = Ball.instance.bounds;
        }
        DetectCollisions();
    }

    void DetectCollisions() {
        //Atualiza posição ocupada pela bola.
        if(Ball.instance != null) {
            playerBounds.center = Ball.instance.transform.position;
        }

        if(Ball.instance.collided) {
            return;
        }
        //Não sobrecarrega colisões.
        if(bounds.Intersects(playerBounds)) {
            Ball.instance.collided = true;
            Ball.instance.initialDir = Reflect(Ball.instance.initialDir, transform.position.normalized) * -1;
            Debug.Log("Colidiu");
            GameController.instance.playerPoints++;
            GameController.instance.barsLeft--;
            Destroy(gameObject);
        }
    }

    Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2 * Vector3.Dot(v, n) * n;
    }
}

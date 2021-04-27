using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    public Transform leftLimit;
    public Transform rightLimit;
    public GameObject ball;
    public Transform spawnPoint;
    [Header("Colisões:")]
    public MeshFilter mesh;
    public Bounds bounds;

    public Bounds playerBounds;

    //Correção de tempo para colidir novamente;
    public float preventCollisionErrorsOffsetTime;
    public bool collided;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        bounds = mesh.mesh.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBall();
        Movement();
        DetectCollisions();
    }

    //Função de movimentação do personagem.
    void Movement() {
        if(Input.GetKey(KeyCode.RightArrow)) {
            if(transform.position.x < rightLimit.position.x)
                transform.position += Vector3.right * velocity * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            if(transform.position.x > leftLimit.position.x)
                transform.position += Vector3.left * velocity * Time.deltaTime;
        }
    }

    Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2 * Vector3.Dot(v, n) * n;
    }

    void DetectCollisions() {
        //Detecta a área ocupada pelo player;
        bounds = mesh.mesh.bounds;
        //Atualiza a área do player;
        bounds.center = transform.position;
        bounds.Expand(new Vector3(4, 1, 1));
        //Detecta área ocupada pela bola.
        playerBounds.center = Ball.instance.transform.position;

        if(Ball.instance.collided) {
            return;
        }

        //Não sobrecarrega colisões.
        if(bounds.Intersects(playerBounds)) {
            collided = true;
            Ball.instance.initialDir = Reflect(Ball.instance.initialDir, transform.position.normalized) * -1;
            Debug.Log("Colidiu");
        }
    }

    private void SpawnBall() {
        if(Ball.instance == null) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                Instantiate(ball, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireMesh(mesh.sharedMesh, 0, transform.position, Quaternion.identity, transform.localScale);
    }
}
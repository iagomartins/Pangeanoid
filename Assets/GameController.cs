using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Variável de pontos do jogador.
    public int playerPoints = 0;
    public int playerHealth = 3;
    public GameObject endGameScreen;
    public GameObject perfectGameComponent;
    public Transform deadPoint;
    public Transform bars;
    public int barsLeft;
    public Text pointsText;
    public Text healthText;
    public Text gameOverScore;
    public bool gameOver;
    public bool perfect;
    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        barsLeft = bars.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        endGameScreen.SetActive(gameOver);
        perfectGameComponent.SetActive(perfect);
        DetectLifeLost();
        SetGameOverWhenGameEnds();
        pointsText.text = "Score: " + playerPoints;
        healthText.text = "Lives: " + playerHealth;
        gameOverScore.text = "Score: " + playerPoints;
    }

    //Desativa a escala de tempo pausando o jogo em Game Over.
    void SetGameOverWhenGameEnds() {
        if(playerHealth <= 0 || barsLeft <= 0) {
            gameOver = true;
            if(barsLeft <= 0) {
                perfect = true;
            }
        }
        if (gameOver) {
            Time.timeScale = 0;
            pointsText.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
        }
    }

    //Detecta colisão com o fundo e retira uma vida do jogador.
    void DetectLifeLost() {
        if(Ball.instance.transform.position.y < deadPoint.position.y) {
            Destroy(Ball.instance.gameObject);
            playerHealth--;
        }
    }

    //Funções para botões

    public void LoadStage(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}

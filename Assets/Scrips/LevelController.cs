using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] int scoore = 0;
    [SerializeField] public int lives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Player player;

    public void OnBrickCollider(Brick brick)
    {
        scoore++;
        RemoveBrick(brick);
        Debug.Log("Score: " + scoore);
        if (scoore == 20)
        {
            Debug.Log("¡Enhorabuena! ¡Has ganado!");
            WaitForSeconds wait = new WaitForSeconds(2);

            #if UNITY_EDITOR
            // Si queremos parar la aplicación desde el editor, basta con poner a false el valor
            // de la propiedad UnityEditor.EditorApplication.isPlaying
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            // Cuando ejecutemos la aplicación fuera del editor, podemos emplear la
            // siguiente línea para finalizarla
            Application.Quit();
            #endif
        }
    }
    private void RemoveBrick(Brick brick)
    {
        Destroy(brick.transform.gameObject);
    }
    public void OnDie(){
        lives--;
        if (lives > 0)
        {
            player.enabled =true;
            StartCoroutine(player.Restart());
        }
        if(lives == 0)
        {
            Debug.Log("Perdiste todas las vidas, reiniciando partida...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Debug.Log("Has muerto");
    }
}

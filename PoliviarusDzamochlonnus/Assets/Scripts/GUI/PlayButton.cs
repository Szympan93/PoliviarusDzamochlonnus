using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(1));
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private GameObject _fadeCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    public Animator animator;
    public Animator fishanimator;
    public Animator fishanimator1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target + .1f, 3 * Time.deltaTime);
    }
    public async void LoadScene(string sceneName, bool useLoader, bool isTutorial = false)
    {
        _target = 0;
        _progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        if (useLoader)
        {
            _loaderCanvas.SetActive(true);
            fishanimator.SetTrigger("On");
            fishanimator1.SetTrigger("On");
        }
        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < .9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        if (useLoader)
            _loaderCanvas.SetActive(false);
        if (!isTutorial)
            animator.SetTrigger("FadeIn");
    }

}

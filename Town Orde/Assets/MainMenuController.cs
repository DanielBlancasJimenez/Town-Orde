using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIDocument))]
public class MainMenuController : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre de la escena a cargar al presionar Jugar")]
    [SerializeField] private string gameSceneName = "GameScene";

    private UIDocument uiDocument;
    private Button btnPlay;
    private Button btnOptions;
    private Button btnExit;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;

        btnPlay = root.Q<Button>("btn-play");
        btnOptions = root.Q<Button>("btn-options");
        btnExit = root.Q<Button>("btn-exit");

        if (btnPlay != null)
            btnPlay.clicked += OnPlayClicked;

        if (btnOptions != null)
            btnOptions.clicked += OnOptionsClicked;

        if (btnExit != null)
            btnExit.clicked += OnExitClicked;
    }

    private void OnDisable()
    {
        if (btnPlay != null)
            btnPlay.clicked -= OnPlayClicked;

        if (btnOptions != null)
            btnOptions.clicked -= OnOptionsClicked;

        if (btnExit != null)
            btnExit.clicked -= OnExitClicked;
    }

    private void OnPlayClicked()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void OnOptionsClicked()
    {
        // Espacio reservado para abrir el submenú de ajustes
    }

    private void OnExitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
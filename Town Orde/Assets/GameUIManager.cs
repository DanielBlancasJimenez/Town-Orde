using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameUIManager : MonoBehaviour
{
    [Header("Configuración de UI")]
    [SerializeField] private StyleSheet styleSheet; // Campo para asignar el .uss

    [Header("Configuración del Grid")]
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 8;

    private UIDocument uiDocument;
    private VisualElement gridContainer;
    private Label infoLabel;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;

        // Inyectar la hoja de estilos de forma forzada
        if (styleSheet != null && !root.styleSheets.Contains(styleSheet))
        {
            root.styleSheets.Add(styleSheet);
        }

        // Referencias a la UI
        gridContainer = root.Q<VisualElement>("grid-container");
        infoLabel = root.Q<Label>(className: "wrap-text");

        if (gridContainer != null)
        {
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        gridContainer.Clear();

        for (int r = 0; r < rows; r++)
        {
            VisualElement rowElement = new VisualElement();
            rowElement.AddToClassList("grid-row");

            if (r == rows - 1)
                rowElement.AddToClassList("grid-row-last");

            for (int c = 0; c < columns; c++)
            {
                Button cell = new Button();
                cell.AddToClassList("grid-cell");

                if (c == columns - 1)
                    cell.AddToClassList("grid-cell-last-col");

                int row = r;
                int col = c;
                cell.name = $"Cell_{row}_{col}";
                
                cell.clicked += () => OnCellClicked(row, col);

                rowElement.Add(cell);
            }

            gridContainer.Add(rowElement);
        }
    }

    private void OnCellClicked(int row, int col)
    {
        Debug.Log($"Casilla seleccionada: [{row}, {col}]");
        
        if (infoLabel != null)
        {
            infoLabel.text = $"Has seleccionado la casilla en la fila {row}, columna {col}.";
        }
    }
}
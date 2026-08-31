using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GridManager : MonoBehaviour
{
    [SerializeField] private StyleSheet styleSheet;
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 8;

    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;
        root.Clear();

        // Cargar y aplicar la hoja de estilos
        if (styleSheet != null)
        {
            root.styleSheets.Add(styleSheet);
        }

        // Contenedor centrado
        VisualElement container = new VisualElement();
        container.AddToClassList("game-container");
        root.Add(container);

        // Tablero cuadrado con bordes redondeados
        VisualElement gridBoard = new VisualElement();
        gridBoard.AddToClassList("grid-board");
        container.Add(gridBoard);

        // Generar filas y columnas
        for (int r = 0; r < rows; r++)
        {
            VisualElement rowElement = new VisualElement();
            rowElement.AddToClassList("grid-row");

            if (r == rows - 1)
            {
                rowElement.AddToClassList("grid-row-last");
            }

            for (int c = 0; c < columns; c++)
            {
                Button cell = new Button();
                cell.AddToClassList("grid-cell");

                if (c == columns - 1)
                {
                    cell.AddToClassList("grid-cell-last-col");
                }

                int row = r;
                int col = c;
                cell.name = $"Cell_{row}_{col}";
                cell.clicked += () => OnCellClicked(row, col);

                rowElement.Add(cell);
            }

            gridBoard.Add(rowElement);
        }
    }

    private void OnCellClicked(int row, int col)
    {
        Debug.Log($"Click en celda: [{row}, {col}]");
    }
}
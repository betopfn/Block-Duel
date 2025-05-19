using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarregarCena : MonoBehaviour
{
    public string nomeDaCenaParaCarregar;
    private Button botao;

    void Start()
    {
        // Tenta encontrar o componente Button neste GameObject
        botao = GetComponent<Button>();

        // Adiciona um listener para o evento de clique do botão
        if (botao != null)
        {
            botao.onClick.AddListener(CarregarNovaCena);
        }
        else
        {
            Debug.LogError("Componente Button não encontrado neste GameObject!");
        }
    }

    void CarregarNovaCena()
    {
        // Carrega a cena com o nome especificado
        SceneManager.LoadScene(nomeDaCenaParaCarregar);
    }
}
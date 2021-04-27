using CadastroAlunos.Models;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CadastroAlunos
{
    /// <summary>
    /// Interaction logic for CadastroAluno.xaml
    /// </summary>
    public partial class CadastroAluno : Window
    {
        public OpenFileDialog ofdXML;

        public OpenFileDialog ofdFoto;

        public Aluno AlunoAtual { get; set; }

        public Aluno[] Alunos { get; set; }

        public CadastroAluno()
        {
            AlunoAtual = new Aluno();

            // Cria um elemento de busca de arquivo XML
            ofdXML = new OpenFileDialog
            {
                Title = "Procure o XML de Alunos",
                Filter = "XML files|*.xml",
                InitialDirectory = @"C:\",
                Multiselect = false
            };

            // Adiciona uma função no evento de selecionar um arquivo XML
            ofdXML.FileOk += OfdXmlOnFileOk;

            // Cria um elemento de busca de arquivo Foto
            ofdFoto = new OpenFileDialog
            {
                Title = "Procure a foto do aluno",
                Filter = "Files|*.jpg;*.jpeg;*.png;",
                InitialDirectory = @"C:\",
                Multiselect = false
            };

            // Adiciona uma função no evento de selecionar um arquivo Foto
            ofdFoto.FileOk += OfdFotoOnFileOk;

            // Inicializa a lista de alunos
            Alunos = new Aluno[] { };

            InitializeComponent();

            gdvAlunos.SelectionChanged += GdvAlunosOnSelectionChanged;

            // Inicializa o Data Grid com a lista de alunos atual
            AtualizarGrid();
        }

        // Evento de seleção do um item no DataGrid
        private void GdvAlunosOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(gdvAlunos.SelectedItem is Aluno item)) return;

            AlunoAtual = item;

            txtCodigo.Text = item.Codigo.ToString();
            txtCurso.Text = item.Curso.ToString();
            txtMensalidade.Text = item.Mensalidade.ToString(CultureInfo.InvariantCulture);
            txtNome.Text = item.Nome.ToString();

            if (!string.IsNullOrEmpty(item.Foto))
            {
                var caminhoFoto = Path.Combine(Environment.CurrentDirectory, item.Foto);
                var uriFoto = new Uri(caminhoFoto);

                picFoto.Source = new BitmapImage(uriFoto);
            }
        }

        // Evento de seleção de um arquivo de Foto
        private void OfdFotoOnFileOk(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ofdFoto.FileName)) return;

            var destination = Helpers.removerAcentos(ofdFoto.SafeFileName.Replace(" ", string.Empty));

            File.Copy(ofdFoto.FileName, destination, true);

            var caminhoFoto = Path.Combine(Environment.CurrentDirectory, Helpers.removerAcentos(ofdFoto.SafeFileName.Replace(" ", string.Empty)));
            var uriFoto = new Uri(caminhoFoto);

            picFoto.Source = new BitmapImage(uriFoto);
        }

        // Evento de seleção de um arquivo XML
        private void OfdXmlOnFileOk(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ofdXML.FileName)) return;

            var serializer = new XmlSerializer(typeof(DocumentoXMLAlunos));

            using Stream reader = new FileStream(ofdXML.FileName, FileMode.Open);

            var documento = (DocumentoXMLAlunos)serializer.Deserialize(reader);

            Alunos = documento.Alunos;

            AtualizarGrid();
        }

        // Atualiza o DataGrid com o array atualizado de Alunos
        private void AtualizarGrid()
        {
            gdvAlunos.ItemsSource = new Aluno[]{};
            gdvAlunos.ItemsSource = Alunos;
        }

        // Clique no botão que abre pra selecionar o arquivo XML
        private void BtnCarregaXML_OnClick(object sender, RoutedEventArgs e)
        {
            ofdXML.ShowDialog();
        }

        // Função que valida o formulário antes de inserir ou atualizar um registro
        private void ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                ExibirErro("Preencha o campo Código");
                return;
            }

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                ExibirErro("Preencha o campo Nome");
                return;
            }

            if (string.IsNullOrEmpty(txtCurso.Text))
            {
                ExibirErro("Preencha o campo Curso");
                return;
            }

            if (string.IsNullOrEmpty(txtMensalidade.Text))
            {
                ExibirErro("Preencha o campo Mensalidade");
                return;
            }
        }

        // Clique no botão que Incluir aluno
        private void BtnInlcuir_OnClick(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();

            AlunoAtual.Nome = txtNome.Text;
            AlunoAtual.Curso = txtCurso.Text;
            AlunoAtual.Mensalidade = decimal.Parse(txtMensalidade.Text);
            AlunoAtual.Codigo = int.Parse(txtCodigo.Text);

            AlunoAtual.Foto = (picFoto.Source as BitmapImage)?.UriSource.Segments.LastOrDefault();

            Alunos = Alunos.Append(AlunoAtual).ToArray();

            LimparAlunoAtual();

            AtualizarGrid();
        }

        // Clique no botão que limpar os dados do formulário
        private void LimparAlunoAtual()
        {
            AlunoAtual = new Aluno();

            txtNome.Text = string.Empty;
            txtCurso.Text = string.Empty;
            txtMensalidade.Text = string.Empty;
            txtCodigo.Text = string.Empty;

            picFoto.Source = null;

            EsconderErro();
        }

        // Clique no botão que abre a pesquisa para selecionar um arquivo de foto
        private void BtnLocalizarFoto_OnClick(object sender, RoutedEventArgs e)
        {
            ofdFoto.ShowDialog();
        }

        // Clique no botão que Limpar formulário
        private void BtnLimpar_OnClick(object sender, RoutedEventArgs e)
        {
            LimparAlunoAtual();
        }

        // Função que exibe uma mensagem de erro
        private void ExibirErro(string erro)
        {
            lblErro.Content = erro;
            lblErro.Visibility = Visibility.Visible;
        }

        // Função que esconde a mensagem de erro
        private void EsconderErro()
        {
            lblErro.Content = string.Empty;
            lblErro.Visibility = Visibility.Hidden;
        }

        // Clique no botão que faz a pesquisa no DataGrid
        private void BtnProcurar_OnClick(object sender, RoutedEventArgs e)
        {
            var resultados = (gdvAlunos.ItemsSource as Aluno[]).ToList();

            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                if (resultados.Count > 0)
                {
                    resultados = resultados.Where(x => x.Codigo == int.Parse(txtCodigo.Text)).ToList();
                }
            }

            if (!string.IsNullOrEmpty(txtNome.Text))
            {
                if (resultados.Count > 0)
                {
                    resultados = resultados.Where(x => x.Nome.ToLower().Contains(txtNome.Text.ToLower())).ToList();
                }
            }

            if (!string.IsNullOrEmpty(txtCurso.Text))
            {
                if (resultados.Count > 0)
                {
                    resultados = resultados.Where(x => x.Curso.ToLower().Contains(txtCurso.Text.ToLower())).ToList();
                }
            }

            if (!string.IsNullOrEmpty(txtMensalidade.Text))
            {
                if (resultados.Count > 0)
                {
                    resultados = resultados.Where(x => x.Mensalidade == decimal.Parse(txtMensalidade.Text)).ToList();
                }
            }

            if (resultados.Count > 0)
            {
                var itemSelecionado = resultados.First();

                gdvAlunos.SelectedItem = itemSelecionado;
            }
            else
            {
                ExibirErro("Nenhum Aluno encontrado");
            }
        }

        // Clique no botão que atualiza um registro no Data Grid
        private void BtnAtualizar_OnClick(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();

            var posicaoAlunoAtual = Alunos.ToList().IndexOf(AlunoAtual);

            AlunoAtual.Codigo = int.Parse(txtCodigo.Text);
            AlunoAtual.Nome = txtNome.Text;
            AlunoAtual.Curso = txtCurso.Text;
            AlunoAtual.Mensalidade = decimal.Parse(txtMensalidade.Text);
            AlunoAtual.Foto = (picFoto.Source as BitmapImage)?.UriSource.Segments.LastOrDefault();

            Alunos[posicaoAlunoAtual] = AlunoAtual;

            AtualizarGrid();
        }

        // Clique no botão que deleta um registro do DataGrid
        private void BtnDeletar_OnClick(object sender, RoutedEventArgs e)
        {
            if (Alunos.Length <= 0) return;

            var posicaoAlunoAtual = Alunos.ToList().IndexOf(AlunoAtual);

            var listaAlunos = Alunos.ToList();

            listaAlunos.RemoveAt(posicaoAlunoAtual);

            Alunos = listaAlunos.ToArray();

            AtualizarGrid();
        }

        // Clique no botão que volta para o primeiro registro
        private void BtnPrimeiro_OnClick(object sender, RoutedEventArgs e)
        {
            if (Alunos.Length <= 0) return;

            gdvAlunos.SelectedItem = Alunos.First();
        }

        // Clique no botão que avança para o último registro 
        private void BtnUltimo_OnClick(object sender, RoutedEventArgs e)
        {
            if (Alunos.Length <= 0) return;

            gdvAlunos.SelectedItem = Alunos.Last();
        }

        // Clique no botão que volta para o registro anterior
        private void BtnAnterior_OnClick(object sender, RoutedEventArgs e)
        {
            if (Alunos.Length <= 1) return;

            if (gdvAlunos.SelectedItem == null) return;

            var posicaoAluno = Alunos.ToList().IndexOf(gdvAlunos.SelectedItem as Aluno);

            if (posicaoAluno == 0) return;

            gdvAlunos.SelectedItem = Alunos[posicaoAluno - 1];
        }

        // Clique no botão que avança para o próximo registro
        private void BtnProximo_OnClick(object sender, RoutedEventArgs e)
        {
            if (Alunos.Length <= 1) return;

            if (gdvAlunos.SelectedItem == null) return;

            var posicaoAluno = Alunos.ToList().IndexOf(gdvAlunos.SelectedItem as Aluno);

            if (posicaoAluno == Alunos.Length - 1) return;

            gdvAlunos.SelectedItem = Alunos[posicaoAluno + 1];
        }

        // Clique no botão que Fecha o programa
        private void BtnSair_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

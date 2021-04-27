using System.Xml.Serialization;

namespace CadastroAlunos.Models
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "alunos")]
    public partial class DocumentoXMLAlunos
    {
        [XmlElement("aluno")]
        public Aluno[] Alunos { get; set; }
    }
}
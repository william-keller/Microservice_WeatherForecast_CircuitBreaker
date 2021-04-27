using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CadastroAlunos.Models
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Aluno
    {
        [XmlElement("codigo")]
        public int Codigo { get; set; }

        [XmlElement("nome")]
        public string Nome { get; set; }

        [XmlElement("curso")]
        public string Curso { get; set; }

        [XmlElement("mensalidade")]
        public decimal Mensalidade { get; set; }

        [XmlElement("foto")]
        public string Foto { get; set; }
    }
}

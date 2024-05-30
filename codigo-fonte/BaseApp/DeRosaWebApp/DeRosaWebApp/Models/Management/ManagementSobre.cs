using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models.Management
{
    public class ManagementSobre
    {
        public int Id { get; set; }
        public string TituloSobre { get; set; }
        public string TextoSobre { get; set; }
        public string ImagemUrl { get; set; }
        public string Font { get; set; }
        public string Color { get; set; }
    }
}

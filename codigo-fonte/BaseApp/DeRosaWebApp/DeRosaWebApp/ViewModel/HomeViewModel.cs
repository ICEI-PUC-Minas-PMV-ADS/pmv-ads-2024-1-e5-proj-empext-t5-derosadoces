using DeRosaWebApp.Models;
using DeRosaWebApp.Models.Management;

namespace DeRosaWebApp.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public ManagementHome ManagementHome { get; set; }
    }
}

using System.Collections.Generic;

namespace Doi_Project.ViewModel
{
    public class ClientResultViewModel
    {
        public List<ClientViewModel> Data { get; set; } = new List<ClientViewModel>();
        public MetaViewModel Meta { get; set; } = new MetaViewModel();
    }
}

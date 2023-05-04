using System.Collections.Generic;

namespace Doi_Project.ViewModel
{
    public class ResultViewModel
    {
        public List<ProviderViewModel> Data { get; set; } = new List<ProviderViewModel>();
        public MetaViewModel Meta { get; set; } = new MetaViewModel();

    }

    public class ProviderViewModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public AttributeViewModel Attributes { get; set; } = new AttributeViewModel();
        public RelationshipsViewModel Relationships { get; set; } = new RelationshipsViewModel();

    }
    public class AttributeViewModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Created { get; set; }

    }
    public class RelationshipsViewModel
    {
        public ClientsViewModel Clients { get; set; } = new ClientsViewModel();
    }
    public class ClientsViewModel
    {
        public List<ClientViewModel> Data { get; set; } = new List<ClientViewModel>();
    }
    public class ClientViewModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}

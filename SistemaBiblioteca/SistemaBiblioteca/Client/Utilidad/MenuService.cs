using MudBlazor;

namespace SistemaBiblioteca.Client.Utilidad
{
    public class MenuService
    {
        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
                new BreadcrumbItem("Personal", href: null),
                new BreadcrumbItem("Dashboard", href:null),
         };

        public List<BreadcrumbItem> GetMenu
        {
            get { return _items; }
        }

        public void SetMenu(BreadcrumbItem item)
        {
            _items.Clear();
            _items.Add(item);
        }


    }
}

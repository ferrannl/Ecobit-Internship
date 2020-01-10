using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage
{
    public class ViewUriFactory
    {
        public IEnumerable<string> ViewUris
        {
            get { return _uris.Keys; }
        }

        private Dictionary<string, Uri> _uris;

        public ViewUriFactory()
        {
            _uris = new Dictionary<string, Uri>();
            _uris["UserPrivilegeView"] = new Uri("UserPrivilegeListView.xaml", UriKind.Relative);
            _uris["AccountView"] = new Uri("AccountView.xaml", UriKind.Relative);
            _uris["LoginView"] = new Uri("LoginView.xaml", UriKind.Relative);
            _uris["UserListView"] = new Uri("UserListView.xaml", UriKind.Relative);
            _uris["PrivilegeListView"] = new Uri("PrivilegeListView.xaml", UriKind.Relative);
            _uris["UserEditView"] = new Uri("UserEditView.xaml", UriKind.Relative);
            _uris["PrivilegeEditView"] = new Uri("PrivilegeEditView.xaml", UriKind.Relative);
            _uris["UserPrivilegeListView"] = new Uri("UserPrivilegeListView.xaml", UriKind.Relative);
            _uris["UserPrivilegeEditView"] = new Uri("UserPrivilegeEditView.xaml", UriKind.Relative);
            _uris["ChangePasswordView"] = new Uri("ChangePasswordView.xaml", UriKind.Relative);
        }

        public Uri GetUri(string page)
        {
            if (ViewUris.Contains(page))
            {
                return _uris[page];
            }
            return null;
        }
    }
}

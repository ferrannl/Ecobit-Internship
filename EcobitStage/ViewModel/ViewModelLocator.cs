using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace EcobitStage.ViewModel
{
    public class ViewModelLocator
    {
        //test
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Viewmodel
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<UserPrivilegeViewModel>();
            SimpleIoc.Default.Register<PrivilegeViewModel>();
            SimpleIoc.Default.Register<ChangePasswordViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();

        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public UserPrivilegeViewModel UserPrivilegeList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserPrivilegeViewModel>();
            }
        }
        public PrivilegeViewModel PrivilegeList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PrivilegeViewModel>();
            }
        }
        public UserViewModel UserList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserViewModel>();
            }
        }
        public ChangePasswordViewModel ChangePassword
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangePasswordViewModel>();
            }
        }
    }
}
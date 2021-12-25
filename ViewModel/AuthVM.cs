using CourseWork.Model;
using CourseWork.View;
using System.Windows;
using CourseWork.Utils;
using CourseWork.Model.Data.Service;

namespace CourseWork.ViewModel
{
    public class AuthVM : BaseVM
    {
        public AuthVM()
        {
            Lang = lang;
        }

        #region PROPERTIES
        public static string Nickname { get; set; }
        public string Password { get; set; }



        private static string lang = "en-US";
        public static string Lang
        {
            get => lang;
            set { lang = value; }
        }

        #endregion

        #region METHODS
        private void _OpenMainWnd()
        {
            MainWindow mainWindow = new();
            if (UserService.AuthUser(Nickname, Password))
                CommonUtil.OpenWindow(mainWindow);
            else MessageBox.Show("WRONG DATA!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void _SwitchLang()
        {
            switch (lang)
            {
                case "ru-RU":
                    lang = "en-US";
                    break;
                case "en-US":
                    lang = "ru-RU";
                    break;
            }
            MessageBox.Show($"Язык был успешно сменён на {Lang}");
        }
        #endregion

        #region COMMANDS

        private readonly RelayCommand openMainWnd;
        public RelayCommand OpenMainWnd { get => openMainWnd ?? new(o => _OpenMainWnd()); }


        private RelayCommand switchLang;
        public RelayCommand SwitchLang { get => switchLang ?? new(o => _SwitchLang()); }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public static class Mediator
    {
        private static bool _isChecked;
        private static bool _isLanguageChecked;
        private static int _isUserLogged;

        public static event EventHandler<bool> IsCheckedChanged;
        public static event EventHandler<bool> IsLanguageCheckedChanged;
        public static event EventHandler<int> IsUserLogged;

        public static void OnIsCheckedChanged(bool isChecked)
        {
            _isChecked = isChecked;
            IsCheckedChanged?.Invoke(null, isChecked);
        }

        public static void OnIsLanguageCheckedChanged(bool isLanguageChecked)
        {
            _isLanguageChecked = isLanguageChecked;
            IsLanguageCheckedChanged?.Invoke(null, isLanguageChecked);
        }

        public static void OnIsUserLogged(int isUserLogged)
        {
            _isUserLogged = isUserLogged;
            IsUserLogged?.Invoke(null, isUserLogged);
        }

        public static int GetIsUserLogged()
        {
            return _isUserLogged;
        }

        public static bool GetCurrentIsChecked()
        {
            return _isChecked;
        }

        public static bool GetCurrentIsLanguageChecked()
        {
            return _isLanguageChecked;
        }
    }
}

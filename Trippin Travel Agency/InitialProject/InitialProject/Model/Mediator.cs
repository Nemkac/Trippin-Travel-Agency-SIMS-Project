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

        public static event EventHandler<bool> IsCheckedChanged;
        public static event EventHandler<bool> IsLanguageCheckedChanged;

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

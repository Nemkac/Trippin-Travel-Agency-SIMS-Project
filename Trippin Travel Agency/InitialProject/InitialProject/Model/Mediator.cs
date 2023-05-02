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

        public static event EventHandler<bool> IsCheckedChanged;

        public static void OnIsCheckedChanged(bool isChecked)
        {
            _isChecked = isChecked;
            IsCheckedChanged?.Invoke(null, isChecked);
        }

        public static bool GetCurrentIsChecked()
        {
            return _isChecked;
        }
    }
}

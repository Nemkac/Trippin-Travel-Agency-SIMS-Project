using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoTutorialViewModel : ViewModelBase
    {
        private string control;
        public string Control
        {
            get { return control; }
            set
            {
                if (control != value)
                {
                    control = value;
                    OnPropertyChanged(nameof(Control));
                }


            }
        }
        public ViewModelCommand Play  { get; private set; }

        public ViewModelCommand Pause { get; private set; }
        public  GuestTwoTutorialViewModel() 
        {
            Control = "Play";
            Play = new ViewModelCommand(PlayVideo);
            Pause = new ViewModelCommand(PauseVideo);
        }

        public void PlayVideo(object obj) 
        {
            Control = "Play";
        }
        public void PauseVideo(object obj)
        {
            Control = "Pause";
        }
    }
}

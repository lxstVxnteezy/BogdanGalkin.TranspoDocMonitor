using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranspoDocMonitor.Desktop.Common.Base;

namespace TranspoDocMonitor.Desktop.User.Model
{
    public class User : BaseViewModel
    {
        private bool isSelected;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                SetProperty(ref isSelected, value);
            }
        }
    }
}

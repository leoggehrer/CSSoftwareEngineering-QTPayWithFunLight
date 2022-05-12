using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QTPayWithFunLight.WpfApp.ViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        public ObservableCollection<Logic.Entities.Payment> Payments
        {
            get
            {
                using var ctrl = new Logic.Controllers.PaymentsController();
                var entities = Task.Run(async () => await ctrl.QueryByAsync(cardNumber, year, month, day).ConfigureAwait(false)).Result;
                var volumne = Task.Run(async () => await ctrl.QueryVolumeByAsync(cardNumber, year, month, day).ConfigureAwait(false)).Result;

                Volumne = volumne;
                OnPropertyChanged(nameof(Volumne));
                return new ObservableCollection<Logic.Entities.Payment>(entities);
            }
        }
        private int windowHeight = 700;
        public int WindowHeight
        {
            get
            {
                return windowHeight;
            }
            set
            {
                windowHeight = value;
            }
        }
        private int windowWidth = 800;
        public int WindowWidth
        {
            get
            {
                return windowWidth;
            }
            set
            {
                windowWidth = value;
                OnPropertyChanged(nameof(DataItemWidth));
            }
        }
        public int DataItemWidth
        {
            get
            {
                return windowWidth - 100;
            }
        }

        public decimal Volumne { get; set; }


        private string? cardNumber;
        public string? CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                cardNumber = value;
                OnPropertyChanged(nameof(Payments));
            }
        }
        private int? year;
        public string Year
        {
            get
            {
                return year != null ? year.ToString()! : string.Empty;
            }
            set
            {
                if (int.TryParse(value, out int val))
                {
                    year = val;
                }
                else
                {
                    year = null;
                }
                OnPropertyChanged(nameof(Payments));
            }
        }
        private int? month;
        public string Month
        {
            get
            {
                return month != null ? month.ToString()! : string.Empty;
            }
            set
            {
                if (int.TryParse(value, out int val))
                {
                    month = val;
                }
                else
                {
                    month = null;
                }
                OnPropertyChanged(nameof(Payments));
            }
        }
        private int? day;
        public string Day
        {
            get
            {
                return day != null ? day.ToString()! : string.Empty;
            }
            set
            {
                if (int.TryParse(value, out int val))
                {
                    day = val;
                }
                else
                {
                    day = null;
                }
                OnPropertyChanged(nameof(Payments));
            }
        }

        private ICommand? commandAdd;

        public ICommand CommandAdd
        {
            get
            {
                return RelayCommand.CreateCommand(ref commandAdd, p =>
                {
                },
                p => true);
            }
        }
    }
}

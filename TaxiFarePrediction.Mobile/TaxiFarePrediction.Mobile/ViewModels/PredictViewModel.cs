using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using TaxiFarePrediction.Mobile.Models;
using TaxiFarePrediction.Mobile.Services;

namespace TaxiFarePrediction.Mobile.ViewModels
{
    public class PredictViewModel : BaseViewModel
    {
        private string vendorId;

        public string VendorId
        {
            get { return vendorId; }
            set { SetProperty(ref vendorId, value); }
        }

        private string rateCode;

        public string RateCode
        {
            get { return rateCode; }
            set { SetProperty(ref rateCode, value); }
        }


        private float passengerCount;

        public float PassengerCount
        {
            get { return passengerCount; }
            set { SetProperty(ref passengerCount, value); }
        }

        private float tripTime;

        public float TripTime
        {
            get { return tripTime; }
            set { SetProperty(ref tripTime, value); }
        }

        private float tripDistance;

        public float TripDistance
        {
            get { return tripDistance; }
            set { SetProperty(ref tripDistance, value); }
        }

        private float fareAmount;

        public float FareAmount
        {
            get { return fareAmount; }
            set { SetProperty(ref fareAmount, value); }
        }

        private string paymentType;

        public string PaymentType
        {
            get { return paymentType; }
            set { SetProperty(ref paymentType, value); }
        }

        private TaxiTrip trip;

        public TaxiTrip Trip
        {
            get { return trip; }
            set { SetProperty(ref trip, value); }
        }

        public ICommand PredictCommand { private set; get; }

        private async Task Predict()
        {
            Trip = new TaxiTrip()
            {
                FareAmount = FareAmount,
                PassengerCount = PassengerCount,
                PaymentType = PaymentType,
                RateCode = RateCode,
                TripDistance = TripDistance,
                TripTime = TripTime,
                VendorId = VendorId
            };

            var amount = await PredictionService.Predict(Trip);
            await App.Current.MainPage.DisplayAlert("Prediction", $"Trip Fare: {amount:C2}", "OK");
        }

        public PredictViewModel()
        {
            FareAmount = 0;
            PassengerCount = 1;
            PaymentType = "CRD";
            RateCode = "1";
            TripDistance = 3.75f;
            TripTime = 1140;
            VendorId = "VTS";

            PredictCommand = new Command(async () => await Predict());
        }
    }
}
using Microsoft.VisualBasic;
using Iot.Device.DHTxx;
using System.Device.Gpio;
using Microsoft.AspNetCore.SignalR;


namespace Raspberry.Models
{
    public class GpioClient : IDisposable
    {
        private const int RelayPin = 18;//Relay control pin
        private const int DhtPin = 17;//Dht11 sensor pin

        private GpioController _controller = new GpioController();
        private bool disposedValue = false;
        private object _locker = new object();

        public GpioClient()
        {
            _controller.OpenPin(RelayPin, PinMode.Output);
            _controller.Write(RelayPin, PinValue.Low);
        }



        public void RelayOn()
        {
            lock (_locker)
            {
                _controller.Write(RelayPin, PinValue.High);
            }
        }

        public void RelayOff()
        {
            lock (_locker)
            {
                _controller.Write(RelayPin, PinValue.Low);
            }
        }

        public TemperatureData GetTempData()
        {
            using (var dht = new Dht11(DhtPin, PinNumberingScheme.Logical))
            {
               
                var temperature = new TemperatureData
                {
                    Temperature = dht.Temperature.DegreesCelsius,
                    Humidity = dht.Humidity.Percent,
                    IsLastReadSuccessful = dht.IsLastReadSuccessful,
                };
                return temperature;
                //_hubContext.Clients.All.SendAsync("ReceiveDhtStatus", $"Temperature: {_dht.Temperature.Celsius.ToString("0.0")} °C, Humidity: {_dht.Humidity.ToString("0.0")} %");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _controller.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

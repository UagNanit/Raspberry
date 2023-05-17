﻿namespace Raspberry.Models
{
    public class TemperatureData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool IsLastReadSuccessful { get; set; }
    }
}

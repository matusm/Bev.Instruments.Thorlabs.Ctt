namespace Bev.Instruments.Thorlabs.Ctt
{
    public interface IArraySpectrometer
    {
        string InstrumentManufacturer { get; }
        string InstrumentType { get; }
        string InstrumentSerialNumber { get; }
        string InstrumentFirmwareVersion { get; }
        
        double[] Wavelengths { get; }        
        double MinimumWavelength { get; }
        double MaximumWavelength { get; }
        
        double[] GetIntensityData();
        void SetIntegrationTime(double seconds);
        double GetIntegrationTime();
    }
}

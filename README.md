# Bev.Instruments.Thorlabs.Ctt

Adapter library for Thorlabs compact spectrograph devices (CCT series).  
Provides a simple, synchronous-friendly wrapper (`ThorlabsCct`) around the Thorlabs Compact Spectrograph driver for use with applications expecting an array-spectrometer abstraction.

- Target framework: .NET Framework 4.7.2
- Primary entry type: `Bev.Instruments.Thorlabs.Ctt.ThorlabsCct`
- Driver dependency: `Thorlabs.ManagedDevice.CompactSpectrographDriver` (Thorlabs SDK)

## Features
- Connect to the first available Thorlabs compact spectrograph or by device id.
- Read wavelengths and intensity arrays.
- Control integration (exposure) time and hardware averaging.
- Control shutter and LED indicator.
- Query device metadata (model, serial, firmware, electronics id, start date, temperature, status flags).

## Requirements
- Windows (Thorlabs drivers are Windows-only)
- .NET Framework 4.7.2
- Thorlabs Managed Device SDK (add reference to `Thorlabs.ManagedDevice.CompactSpectrographDriver`)
- Visual Studio 2022 recommended for development

## Installation
1. Clone the repository:
2. Open the solution in Visual Studio 2022.
3. Ensure the Thorlabs SDK assembly is referenced by the project (and available on the machine).
4. __Restore NuGet Packages__ (if applicable) and then __Build__ the solution.

## Quick usage

C# example (console or GUI application):

```csharp
// Example code to connect to the first available compact spectrograph
using Bev.Instruments.Thorlabs.Ctt;

var spectrograph = new ThorlabsCct();
spectrograph.Connect();

// Set integration time to 100ms
spectrograph.SetIntegrationTime(0.1);

// Read spectra
var wavelengths = spectrograph.GetWavelengths();
var intensities = spectrograph.GetIntensities();

```

Notes:
- Times accepted by `SetIntegrationTime` are in seconds; the underlying driver requires milliseconds (the wrapper converts).
- The library uses synchronous wrappers around the Thorlabs async APIs; operations that wait on the device can block the calling thread.

## Troubleshooting
- If `ICompactSpectrographDriver` or the Thorlabs types are not found, verify that the Thorlabs SDK is installed and the assembly reference is present in the project.
- Use Visual Studio __Object Browser__ or __Go To Definition__ to inspect the Thorlabs assembly metadata.
- Ensure device drivers and firmware are up-to-date per Thorlabs instructions.

## Contributing
- Fork, create a feature branch, and submit a pull request.
- Keep changes small and focused; include unit tests where applicable.
- If adding public API changes, update this README and add examples.

## Contact
Create issues on the repository for bugs or feature requests.
using Microsoft.SqlServer.Server;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WIA;

class ScannerService
{
    static void Main()
    {
        try
        {
            var deviceManager = new DeviceManager();
            DeviceInfo scanner = null;

            // Buscar un escáner disponible
            foreach (DeviceInfo info in deviceManager.DeviceInfos)
            {
                if (info.Type == WiaDeviceType.ScannerDeviceType)
                {
                    scanner = info;
                    break;
                }
            }

            if (scanner == null)
            {
                Console.WriteLine("No se encontró un escáner.");
                return;
            }

            Device device = scanner.Connect();
            Item item = device.Items[1];

            // Configurar el formato de imagen
            var imageFile = (ImageFile)item.Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");

            // Guardar la imagen escaneada en una ruta temporal
            string tempPath = Path.Combine(Path.GetTempPath(), "factura.jpg");
            File.WriteAllBytes(tempPath, (byte[])imageFile.FileData.get_BinaryData());

            Console.WriteLine(tempPath); // Enviar la ruta a Flutter
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
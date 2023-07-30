using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using printer_aplication_desktop.utils;
using System;
using System.IO;
using System.Text;

namespace printer_aplication_desktop.components
{
    public class EscPosAdapter : IPrinterEscPos
    {
        private ListTypeConexion typeConexion;
        private EPSON elementDataPrinter;
        private object printer;
        private object hostname;
        private object port;


        public EscPosAdapter(object hostname, object port, ListTypeConexion typeConexion)
        {
            elementDataPrinter = new EPSON();
            this.hostname = hostname;
            this.port = port;
            this.typeConexion = typeConexion;
            Conexion();
        }

        private void Conexion() 
        {
            switch (typeConexion)
            {
                case ListTypeConexion.ImmediateNetworkPrinter:
                    printer = new ImmediateNetworkPrinter(new ImmediateNetworkPrinterSettings()
                    {
                        ConnectionString = $"{hostname}:{port}",
                        PrinterName = "PrinterConnector"
                    });
                    break;
                case ListTypeConexion.SerialPrinter:
                    int Bound = Convert.ToInt32(port.ToString());
                    printer = new SerialPrinter(hostname.ToString(), Bound);
                    break;
                case ListTypeConexion.FilePrinter:
                    printer = new FilePrinter(hostname.ToString());
                    break;
                case ListTypeConexion.SambaPrinter:
                    printer = new SambaPrinter(hostname.ToString(), port.ToString());
                    break;
                default:
                    throw new ArgumentException("Tipo de impresora no válido.");
            }
        }

        public void Print(byte[] dataPrintElement)
        {
            if (printer is ImmediateNetworkPrinter printerNetWork) 
            {
                printerNetWork.WriteAsync(CombinePrinterParameter(dataPrintElement));
            }

            if (printer is SerialPrinter printerSerial)
            {
                printerSerial.Write(CombinePrinterParameter(dataPrintElement));
            }

            if (printer is FilePrinter printerFile)
            {
                printerFile.Write(CombinePrinterParameter(dataPrintElement));
            }

            if (printer is SambaPrinter printerSamba)
            {
                printerSamba.Write(CombinePrinterParameter(dataPrintElement));
            }
        }

        public byte[] CombinePrinterParameter(params byte[][] dataPrinter)
        {
            byte[] builder = null;

            foreach (var byteArray in dataPrinter)
            {
                builder = ByteSplicer.Combine(
                    builder,
                    byteArray);
            }
            return builder;
        }

        public byte[] PrintDataLine(string textPrinter)
        {
            byte[] elementText = CombinePrinterParameter(elementDataPrinter.PrintLine(textPrinter));

            return elementText;
        }

        public byte[] CenterTextPosition()
        {
            byte[] elementCenter = CombinePrinterParameter(elementDataPrinter.CenterAlign());

            return elementCenter;
        }

        public byte[] LeftTextPosition()
        {
            byte[] elementLeft = CombinePrinterParameter(elementDataPrinter.LeftAlign());

            return elementLeft;
        }

        public byte[] RightTextPosition()
        {
            byte[] elementRight = CombinePrinterParameter(elementDataPrinter.RightAlign());

            return elementRight;
        }

        public byte[] BoldTextFont()
        {
            byte[] fontBold = CombinePrinterParameter(elementDataPrinter.SetStyles(PrintStyle.Bold));

            return fontBold;
        }

        public byte[] FontBTextFont()
        {
            byte[] fontB = CombinePrinterParameter(elementDataPrinter.SetStyles(PrintStyle.FontB));

            return fontB;
        }

        public byte[] NoneTextFont()
        {
            byte[] fontNone = CombinePrinterParameter(elementDataPrinter.SetStyles(PrintStyle.None));

            return fontNone;
        }

        public byte[] PrintImageData(string imagePath)
        {
            byte[] dataImagePrinter = CombinePrinterParameter(
                        elementDataPrinter.PrintImage(File.ReadAllBytes(imagePath), true),
                        PrintDataLine(""));

            return dataImagePrinter;
        }

        public byte[] TextInvertedFont(bool modeText)
        {
            byte[] textInvertedPrinter = CombinePrinterParameter(elementDataPrinter.ReverseMode(modeText));

            return textInvertedPrinter;
        }

        public byte[] PrintQRCode(string dataQR)
        {
            byte[] dataQRPrinter = CombinePrinterParameter(elementDataPrinter.PrintQRCode(dataQR));

            return dataQRPrinter;
        }

        public byte[] PrinterCutWidth(int quantity)
        {
            byte[] cutPrinter = CombinePrinterParameter(elementDataPrinter.FullCutAfterFeed(quantity));

            return cutPrinter;
        }

        public byte[] UFT8Encoding(string data) 
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(data);

            return utf8Bytes;
        }
    }
}

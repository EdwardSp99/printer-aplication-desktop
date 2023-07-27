using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using System.IO;

namespace printer_aplication_desktop.utils
{
    public class FilePrinterAdapter : IPrinter
    {
        private FilePrinter printer;
        private EPSON elementDataPrinter;

        public FilePrinterAdapter(string filePath)
        {
            printer = new FilePrinter(filePath);
        }

        public void Print(byte[] dataPrintElement)
        {
            printer.Write(CombinePrinterParameter(dataPrintElement));
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
    }
}

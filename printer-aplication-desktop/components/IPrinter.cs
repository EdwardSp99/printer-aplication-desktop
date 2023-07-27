namespace printer_aplication_desktop.utils
{
    public interface IPrinter
    {
        void Print(byte[] dataPrintElement);

        byte[] CombinePrinterParameter(params byte[][] dataPrinter);

        byte[] PrintDataLine(string textPrinter);

        byte[] CenterTextPosition();

        byte[] LeftTextPosition();

        byte[] RightTextPosition();
        
        byte[] BoldTextFont();
        
        byte[] FontBTextFont();

        byte[] NoneTextFont();

        byte[] PrintImageData(string imagePath);

        byte[] TextInvertedFont(bool data);

        byte[] PrintQRCode(string dataQR);

        byte[] PrinterCutWidth(int quantity);

    }
}

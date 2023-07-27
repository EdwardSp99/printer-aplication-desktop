using System;
using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using ESCPOS_NET.Printers;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace printer_aplication_desktop
{
    public class Printer
    {
        private int width = 42;
        private dynamic printer;
        private string type;
        private int times;
        private dynamic data;
        private object connectorPrinter;

        public Printer(dynamic data)
        {
            this.printer = data.printer;
            this.type = data.type;
            this.data = data.data;
            this.times = (data.times == null) ? 1 : data.times;
        }

        public void PrinterExample() 
        {
            Connect(printer);
            EPSON elementPrinter = new EPSON();

            byte[] dataPrinter = PrintLayout(elementPrinter, type, data);

            if (connectorPrinter is ImmediateNetworkPrinter ethernetPrinter)
            {
                ethernetPrinter.WriteAsync(dataPrinter);
            }
            
            if (connectorPrinter is SerialPrinter serialPrinter)
            {
                serialPrinter.Write(dataPrinter);
            }
        }

        private void Connect(dynamic printer)
        {
            try
            {
                switch (printer.type.ToString())
                {
                    case "ethernet":
                        connectorPrinter = new ImmediateNetworkPrinter(new ImmediateNetworkPrinterSettings() { ConnectionString = $"{printer.name_system}:{printer.port}", PrinterName = "TestPrinter" });
                        break;
                    case "linux-usb":
                        connectorPrinter = new FilePrinter(filePath: printer.name_system);
                        break;
                    default:
                        throw new Exception("Tipo de ticketera no soportado");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private byte[] PrintLayout(EPSON printDocument, string type, dynamic data)
        {
            byte[] result = Header(printDocument, data);
            try
            {
                    switch (type)
                    {
                        case "invoice":
                        result = ByteSplicer.Combine(
                            result,
                            BusinessAdditional(printDocument, data),
                            DocumentLegal(printDocument, type, data),
                            Customer(printDocument, data),
                            Additional(printDocument, data),
                            Items(printDocument, data),
                            Amounts(printDocument, data),
                            AdditionalFooter(printDocument, data),
                            FinalMessage(printDocument, data),
                            StringQR(printDocument, data),
                            printDocument.FullCutAfterFeed(2));
                            break;

                        case "note":
                        result = ByteSplicer.Combine(
                            result,
                            DocumentLegal(printDocument, type, data),
                            Customer(printDocument, data),
                            Additional(printDocument, data),
                            printDocument.FullCutAfterFeed(2));
                            break;

                        case "command":
                        result = ByteSplicer.Combine(
                            result,
                            ProductionArea(printDocument, data),
                            TextBackgroundInverted(printDocument, data),
                            DocumentLegal(printDocument, type, data),
                            Additional(printDocument, data),
                            Items(printDocument, data),
                            printDocument.FullCutAfterFeed(2));
                            break;

                        case "precount":
                        result = ByteSplicer.Combine(
                            result,
                            DocumentLegal(printDocument, type, data),
                            Additional(printDocument, data),
                            Items(printDocument, data),
                            Amounts(printDocument, data),
                            printDocument.FullCutAfterFeed(2));
                        break;

                        case "extra":
                        result = ByteSplicer.Combine(
                            result,
                            TitleExtra(printDocument, data),
                            Additional(printDocument, data),
                            Items(printDocument, data),
                            Amounts(printDocument, data),
                            printDocument.FullCutAfterFeed(2));
                            break;

                        default:
                            throw new Exception("No se pudo conectar con la tiketera");
                    }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        private static byte[] Header(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign(), printDocument.SetStyles(PrintStyle.Bold | PrintStyle.FontB));
            if (data.business.comercialDescription != null)
            {
                if (data.business.comercialDescription.type == "text")
                {
                    result = ByteSplicer.Combine(
                      result,
                      printDocument.PrintLine(data.business.comercialDescription.value.ToString().ToUpper())
                      );
                }
                else if (data.business.comercialDescription.type == "img")
                {
                    string appDirectory = Path.Combine(Directory.GetCurrentDirectory(), "img");

                    string imagePath = Path.Combine(appDirectory, "logo.png");

                    if (!File.Exists(imagePath))
                    {
                        throw new Exception("No se encontró el logo");
                    }

                    result = ByteSplicer.Combine(
                        result,
                        printDocument.PrintImage(File.ReadAllBytes(imagePath), true),
                        printDocument.PrintLine("")
                        );
                }
            }

            if (data.business.description != null)
            {
                result = ByteSplicer.Combine(
                    result,
                    printDocument.PrintLine(data.business.description.ToString())
                    );
            }
            return result;
        }

        private static byte[] BusinessAdditional(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign(), printDocument.SetStyles(PrintStyle.FontB));

            if (data.business.additional == null) 
            {
                return null;
            }

            foreach (string additional in data.business.additional)
            {
                result = ByteSplicer.Combine(
                    result,
                    printDocument.PrintLine(additional)
                    );
            }
            return result;
        }

        private static byte[] DocumentLegal(EPSON printDocument, string type, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.Bold | PrintStyle.FontB));

            switch (type)
            {
                case "invoice":
                case "note":
                case "command":

                    if (data.document != null)
                    {
                        string text = data.document.description.ToString() + "  " + data.document.indentifier.ToString();
                        
                        result = ByteSplicer.Combine(
                            result,
                            printDocument.CenterAlign(),
                            printDocument.PrintLine(text));
                    }
                    else
                    {
                        string text = data.document.ToString() + "  " + data.documentId.ToString();

                        result = ByteSplicer.Combine(
                            result,
                            printDocument.CenterAlign(),
                            printDocument.PrintLine(text));
                    }
                    break;

                case "precount":
                    result = ByteSplicer.Combine(
                            result,
                            printDocument.LeftAlign(),
                            printDocument.PrintLine(data.document.description.ToString()));
                    break;
            }
            return result;
        }

        private static byte[] Customer(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB), printDocument.LeftAlign());

            if (data.customer != null)
            {
                foreach (string row in data.customer)
                {
                    result = ByteSplicer.Combine(
                        result,
                        printDocument.PrintLine(row)
                        );
                }
            }
            else 
            {
                result = ByteSplicer.Combine(
                    result,
                    printDocument.PrintLine("----")
                    );
            }

            return result;
        }

        private static byte[] Additional(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB), printDocument.LeftAlign());

            if (data.additional != null)
            {
                foreach (string item in data.additional)
                {
                    result = ByteSplicer.Combine(
                        result,
                        printDocument.PrintLine(item));
                }
            }
            return result;
        }

        private static byte[] Items(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB), printDocument.LeftAlign());

            if (data.items != null)
            {
                if (data.items.Count > 0 && data.items[0].quantity != null)
                {
                    result = ByteSplicer.Combine(
                        result,
                        printDocument.SetStyles(PrintStyle.Bold),
                        printDocument.PrintLine("CAN".PadRight(4)+"DESCRIPCIÓN".PadRight(30) + "TOTAL".PadRight(2))
                        );
                }
                else
                {
                    result = ByteSplicer.Combine(
                        result,
                        printDocument.SetStyles(PrintStyle.Bold),
                        printDocument.PrintLine("DESCRIPCIÓN".PadRight(36) + "TOTAL".PadRight(2))
                        );
                }

                result = ByteSplicer.Combine(
                    result,
                    printDocument.LeftAlign(),
                    printDocument.SetStyles(PrintStyle.Bold),
                    printDocument.PrintLine(new string('-', 42))
                    );

                foreach (dynamic item in data.items)
                {
                    if (item.description is JArray)
                    {
                        foreach (dynamic descriptionItem in item.description)
                        {
                            result = ByteSplicer.Combine(
                                result,
                                printDocument.SetStyles(PrintStyle.FontB),
                                printDocument.PrintLine(descriptionItem.ToString())
                                );
                        }

                        if (item.totalPrice != null)
                        {
                            string totalPrice = item.totalPrice.ToString("F2");

                            result = ByteSplicer.Combine(
                                result,
                                printDocument.RightAlign(),
                                printDocument.SetStyles(PrintStyle.FontB),
                                printDocument.PrintLine(totalPrice)
                                );
                        }
                    }
                    else
                    {
                        if (item.quantity != null && item.description != null)
                        {
                            string quantity = item.quantity.ToString();
                            string description = item.description.ToString();

                            string totalPrice = "";
                            if (item.totalPrice != null)
                            {
                                totalPrice = item.totalPrice.ToString("F2");
                            }

                            result = ByteSplicer.Combine(
                                result,
                                printDocument.SetStyles(PrintStyle.FontB),
                                printDocument.LeftAlign(),
                                printDocument.PrintLine(" " + quantity.PadRight(4) + description.PadRight(40) + totalPrice.PadLeft(10))
                                );
                        }

                        if (item.commentary != null)
                        {
                            result = ByteSplicer.Combine(
                                result,
                                printDocument.LeftAlign(),
                                printDocument.SetStyles(PrintStyle.FontB),
                                printDocument.PrintLine("  => " + item.commentary.ToString())
                                );

                        }
                    }
                }
                result = ByteSplicer.Combine(
                    result,
                    printDocument.LeftAlign(),
                    printDocument.SetStyles(PrintStyle.Bold),
                    printDocument.PrintLine(new string('-', 42))
                    );
            }
            return result;
        }

        private static byte[] Amounts(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB), printDocument.RightAlign());

            if (data.amounts == null) 
            {
                result = ByteSplicer.Combine(printDocument.PrintLine(""));
                return result;
            }

            var jsonString = JsonConvert.SerializeObject(data.amounts);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            int maxCharacterWidth = CalculateMaxCharacterWidth(dictionary);

            foreach (var kvp in dictionary)
            {
                string field = kvp.Key;
                string value = kvp.Value.ToString();

                int valueLength = (maxCharacterWidth - value.Length);

                string valueFinal = field + " : " + value.PadLeft(maxCharacterWidth, ' ');

                result = ByteSplicer.Combine(
                    result,
                    printDocument.SetStyles(PrintStyle.FontB),
                    printDocument.PrintLine(valueFinal)
                    );
            }

            result = ByteSplicer.Combine(
                result,
                printDocument.LeftAlign(),
                printDocument.SetStyles(PrintStyle.Bold),
                printDocument.PrintLine(new string('-', 42))
                );

            return result;
        }

        private static byte[] AdditionalFooter(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB), printDocument.LeftAlign());

            if (data.additionalFooter == null) 
            {
                result = ByteSplicer.Combine(printDocument.PrintLine(""));
                return result;
            }

            foreach (string footerText in data.additionalFooter)
            {
                result = ByteSplicer.Combine(
                    result,
                    printDocument.SetStyles(PrintStyle.FontB),
                    printDocument.PrintLine(footerText)
                    );
            }

            result = ByteSplicer.Combine(
                result,
                printDocument.LeftAlign(),
                printDocument.SetStyles(PrintStyle.Bold),
                printDocument.PrintLine(new string('-', 42))
                );

            return result;
        }

        private static byte[] FinalMessage(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.SetStyles(PrintStyle.FontB));

            if (data.finalMessage == null) 
            {
                result = ByteSplicer.Combine(printDocument.PrintLine(""));
                return result;
            }
                
            if (data.finalMessage is JArray)
            {
                foreach (string message in data.finalMessage)
                {
                    result = ByteSplicer.Combine(
                        result,
                        printDocument.LeftAlign(),
                        printDocument.PrintLine(message)
                        );
                }
            }
            else
            {
                result = ByteSplicer.Combine(
                    result,
                    printDocument.CenterAlign(),
                    printDocument.PrintLine(data.finalMessage.ToString())
                    );
            }
            return result;
        }

        private static byte[] StringQR(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign());

            if (data.stringQR == null)
            {
                result = ByteSplicer.Combine(printDocument.PrintLine(""));
                return result;
            }

            result = ByteSplicer.Combine(
                result,
                printDocument.PrintQRCode(data.stringQR.ToString())
                );

            return result;
        }

        private static byte[] ProductionArea(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign(), printDocument.SetStyles(PrintStyle.FontB));

            result = ByteSplicer.Combine(
                result,
                printDocument.PrintLine(data.productionArea.ToString())
                );

            return result;
        }

        private static byte[] TitleExtra(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign(), printDocument.SetStyles(PrintStyle.FontB));

            result = ByteSplicer.Combine(
                result,
                printDocument.PrintLine(data.titleExtra.title.ToString()),
                printDocument.PrintLine(data.titleExtra.subtitle.ToString())
                );

            return result;
        }

        private static byte[] TextBackgroundInverted(EPSON printDocument, dynamic data)
        {
            byte[] result = ByteSplicer.Combine(printDocument.CenterAlign(), printDocument.SetStyles(PrintStyle.Bold));

            if (data.textBackgroundInverted == null) 
            {
                result = ByteSplicer.Combine(printDocument.PrintLine(""));
                return result;
            }

            string value = data.textBackgroundInverted;

            result = ByteSplicer.Combine(
                result,
                printDocument.ReverseMode(true),
                printDocument.CenterAlign(),
                printDocument.PrintLine(new string(' ', 15) + value + new string(' ', 15)),
                printDocument.ReverseMode(false)
                );
            return result;
        }

        private static int CalculateMaxCharacterWidth(Dictionary<string, object> dictionary)
        {
            int maxStringLength = 0;

            foreach (var kvp in dictionary)
            {
                string valueString = kvp.Value.ToString();
                int valueLength = valueString.Length;

                if (valueLength > maxStringLength)
                {
                    maxStringLength = valueLength;
                }
            }

            return maxStringLength;
        }
    }
}

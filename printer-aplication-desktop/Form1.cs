using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using printer_aplication_desktop.components;

namespace printer_aplication_desktop
{
    public partial class frmPrinterForm : Form
    {
        private string pathFileImage;

        public frmPrinterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string json = @"{
                ""type"": ""invoice"",
                ""times"": 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"": {
                    ""business"": {
                        ""comercialDescription"": {
                            ""type"": ""text"",
                            ""value"": ""REY DE LOS ANDES""
                        },
                        ""description"": ""EMPRESA DE TRANSPORTES REY DE LOS ANDES S.A.C."",
                        ""additional"": [
                            ""RUC 20450523381 AGENCIA ABANCAY"",
                            ""DIRECCIÓN : Av. Brasil S/N"",
                            ""TELÉFONO : 989290733""
                        ]
                    },
                    ""document"": {
                        ""description"": ""Boleta de Venta\r ELECTRONICA"",
                        ""indentifier"": ""B001 - 00000071""  
                    },
                    ""customer"": [
                    ""ADQUIRIENTE"",
                    ""DNI: 20564379248"",
                    ""FASTWORKX SRL"",
                    ""AV CANADA N 159 ABANCAY ABANCAY APURIMAC""
                    ],
                    ""additional"": [
                        ""FECHA EMISIÓN : 01/10/2019 14:51:26"",
                        ""MONEDA : SOLES"",
                        ""USUARIO : ""
                    ],
                    ""items"": [
                        {
                            ""description"": [
                                ""Ruta : ABANCAY-CHALHUANCA"",
                                ""Embarque : ABANCAY"",
                                ""Destino : CHALHUANCA"",
                                ""Asiento : 2"",
                                ""Pasajero : EMERSON ÑAHUINLLA VELASQUEZ"",
                                ""DNI : 70930383"",
                                ""F. Viaje : 01/10/2019 02:00 PM""
                            ],
                            ""totalPrice"": ""29.00""
                        }
                    ],
                    ""amounts"": {
                        ""Operacion no gravada"": ""29.00"",
                        ""IGV"": 0,
                        ""Total"": ""9.00""
                    },
                    ""finalMessage"": [
                        ""REPRESENTACIÓN IMPRESA COMPROBANTE ELECTRÓNICO"",
                        ""PARA CONSULTAR EL DOCUMENTO VISITA NEXUS"",
                        ""HTTPS://NEXUS.FASTWORKX.COM/20450523381"",
                        ""RESUMEN: null"",
                        """",
                        ""POR FASTWORKX S.R.L. - PERÚ""
                    ],
                    ""stringQR"": ""20450523381|03|B001 - 00000071|0|9.00|01/10/2019|6|[object Object]|""
                }
            }";*/

            /*string json = @"{
                ""type"": ""invoice"",
                ""times"": 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"": {
                    ""business"": {
                        ""comercialDescription"": {
                            ""type"": ""text"",
                            ""value"": ""REY DE LOS ANDES""
                        },
                        ""description"": ""EMPRESA DE TRANSPORTES REY DE LOS ANDES S.A.C."",
                        ""document"": ""RUC"",
                        ""documentNumber"": ""20450523381""
                    },
                    ""document"": {
                        ""description"": ""Control de"",
                        ""indentifier"": ""B001 - 00000071""  
                    },
                    ""additional"": [
                        ""FECHA EMISIÓN : 01/10/2019 14:51:26"",
                        ""USUARIO : admin""
                    ],
                    ""items"": [
                        {
                            ""description"": [
                                ""Embarque : ABANCAY"",
                                ""Destino : CHALHUANCA"",
                                ""Asiento : 2"",
                                ""Pasajero : EMERSON ÑAHUINLLA VELASQUEZ"",
                                ""F. Viaje : 01/10/2019 02:00 PM"",
                                ""Conductor : QUISPE CONTRERAS GUILLERMO"",
                                ""Bus : TOYOTA  HIACE PLACA D6R-954"",
                                """"
                            ],
                            ""totalPrice"": ""9.00""
                        }
                    ],
                    ""finalMessage"": ""*** CONTROL DE BUS ***""
                }
            }";*/

            string json = @"{
                ""type"": ""invoice"",
                ""times"" : 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"" : {
                        ""document"": {
                            ""description"": ""Boleta de Venta\r ELECTRONICA"",
                            ""indentifier"": ""B001 - 00000071""  
                        },
	                ""business"": {
	                    ""comercialDescription"": {
	                        ""type"": ""text"",
	                        ""value"": ""REY DE LOS ANDES""
	                    },
	                    ""description"": ""EMPRESA DE TRANSPORTES REY DE LOS ANDES S.A.C."",
	                    ""additional"": [
            	            ""RUC 20450523381 AGENCIA ABANCAY"",               
            	            ""DIRECCIÓN : Av. Brasil S/N"",
                            ""TELÉFONO : 989290733""
	                    ]
	                },
	                ""customer"": [
	    	            ""REMITENTE / CLIENTE"",
	    	            ""DNI: 20564379248"",
	                    ""FASTWORKX SRL"",
	                    ""AV CANADA N 159 ABANCAY ABANCAY APURIMAC""
	                ],
	                ""additional"": [
	                    ""FECHA EMISIÓN : 01/10/2019 14:51:26"",
	                    ""MONEDA : SOLES"",
	                    ""CONSIGNADO : RENZO ZABALA""
                    ],
	                ""items"": [
	                    {
	                        ""description"": ""Tipo : Cajas cerradas"",
	                        ""quantity"": 2,
	                        ""totalPrice"": ""20.00""
	                    },
	                    {
	                        ""description"": ""Giro de dinero"",
	                        ""quantity"": 1,
	                        ""totalPrice"": ""5.00""
	                    }
	                ],
	                ""amounts"": {
	                    ""Operacion no gravada"": ""25.00"",
	                    ""IGV"": 0,
	                    ""Total"": ""25.00""
	                },
	                ""additionalFooter"" :[
	    	            ""FECHA IMPR: 02/10/2019 16:12:34"",
	                    ""USUARIO : ADMIN | AGENCIA : ABANCAY""	
	                ],
	                ""finalMessage"": [
	    	            ""REPRESENTACIÓN IMPRESA DE FACTURA ELECTRÓNICA"",
	    	            ""PARA CONSULTAR EL DOCUMENTO VISITA NEXUS:"",
	    	            ""HTTPS://NEXUS.FASTWORKX.COM/20450523381"",
	    	            ""RESUMEN: Bfdfg+sdfsAfKfVs="",
	    	            """",
	    	            ""POR FASTWORKX S.R.L. - PERÚ""
    	            ],
	                ""stringQR"": ""20450523381|01|F001|00000006|0|9.00|30/09/2019|6|sdfsdfsdf|""
                }
            }";

            /*string json = @"{
                ""type"": ""extra"",
                ""times"": 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"": {
                    ""business"": {
                        ""description"": ""Restaurant H. Pollos""
                    },
                    ""titleExtra"": {
                        ""title"": ""DELIVERY : D-1"",
                        ""subtitle"": ""26-08-2020 14:40:30""
                    },
                    ""additional"": [
                        ""FUENTE: INTERNET"",
                        ""CLIENTE: EMERSON ÑAHUINLLA VELASQUEZ"",
                        ""DIRECCIÓN: AV VILLA EL SOL MZ E LT O"",
                        ""CELULAR : 983780014"",
                        ""REFERENCIA : DESVIO DE TIERRA DESPUES DE MECANICA DE MOTOS"",
                        ""PAGARA : 100.00""
                    ],
                    ""items"": [
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (FAMILIAR)"",
                            ""commentary"" : ""con arto quesooo"",
                            ""totalPrice"" : 14.50 
                        },
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (PERSONAL)"",
                            ""totalPrice"" : 14.50
                        }
                    ]
                }
            }";*/

            /*string json = @"{
                ""type"": ""precount"",
                ""times"": 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"": {
                    ""business"": {
                        ""description"": ""Restaurant H. Pollos""
                    },
                    ""document"": {
                        ""description"": ""PRECUENTA""
                    },
                    ""additional"": [
                        ""FECHA EMISIÓN : 01/10/2019 14:51:26"",
                        ""Mesero(a) : Luis"",
                        ""Mesa : Delivery""
                    ],
                    ""items"": [
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (FAMILIAR)"",
                            ""totalPrice"" : 14.50 
                        },
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (PERSONAL)"",
                            ""totalPrice"" : 14.50
                        }
                    ],
	             ""amounts"": {
	                 ""Total"": ""29.00""
	              }
                }
            }";*/

            /*string json = @"{
                ""type"": ""command"",
                ""times"": 1,
                ""printer"": {
                    ""type"": ""ethernet"",
                    ""name_system"": ""192.168.1.241"",
                    ""port"": ""9100""
                },
                ""data"": {
                    ""business"": {
                        ""description"": ""Restaurant H. Pollos""
                    },
                    ""productionArea"": ""Pizzeria Horno"",
                    ""textBackgroundInverted"" : ""ANULACION"",

                        ""document"": {
                            ""description"": ""COMANDA : "",
                            ""indentifier"": ""71""  
                        },
                    ""additional"": [
                        ""FECHA EMISIÓN : 01/10/2019 14:51:26"",
                        ""Mesero(a) : Luis"",
                        ""Mesa : Delivery""
                    ],
                    ""items"": [
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (FAMILIAR)"",
                            ""commentary"" : ""con arto queso""
                        },
                        {
                            ""quantity"": 1,
                            ""description"": ""HAWAYANA (PERSONAL)""
                        }
                    ]
                }
            }";*/

            /*if (!string.IsNullOrEmpty(pathFileImage))
            {
                string fileDestiny = Path.Combine(Directory.GetCurrentDirectory(), "img");

                createFolderImage();

                string nameFileDestiny = Path.Combine(fileDestiny, "logo" + Path.GetExtension(pathFileImage));

                try
                {
                    if (File.Exists(nameFileDestiny)) 
                    {
                        File.Delete(nameFileDestiny);
                    }

                    File.Copy(pathFileImage, nameFileDestiny);
                    MessageBox.Show("Imagen guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/

            dynamic myData = JsonConvert.DeserializeObject<dynamic>(json);

            /*Printer ethernetPrinter = new Printer(myData);

            ethernetPrinter.PrinterExample();*/

            PrinterClass connectorPrinterFinal = new PrinterClass(myData);

            connectorPrinterFinal.PrinterDocument();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivo de imagen (*.png)|*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathFileImage = openFileDialog.FileName;
                MessageBox.Show(pathFileImage);
            }
        }

        private void createFolderImage() 
        {
           string fileDestinyOriginal = Path.Combine(Directory.GetCurrentDirectory(), "img");

            // Crear la carpeta "img" si no existe.
            if (!Directory.Exists(fileDestinyOriginal))
            {
                Directory.CreateDirectory(fileDestinyOriginal);
            }
        }
    }
}

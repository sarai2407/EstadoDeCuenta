using EstadoCuenta.Api.DTOs;
using iTextSharp.text.pdf;

namespace EstadoCuenta.Api.iTextSharp
{
    public class estadoCuentaPdf
    {
        public static string FechaFormatoddMMyyy(string fecha)
        {
            var data = string.Empty;
            bool vDate = !(fecha == "0000-00-00");
            try
            {
                if (fecha != "" && vDate)
                {
                    DateTime newfechas = DateTime.Parse(fecha);
                    data = String.Format("{0:dd/MM/yyyy}", newfechas);
                }
                else
                {
                    data = "00.00.0000";
                }
            }
            catch (Exception)
            {
                data = "0000.00.00";
            }
            return data;
        }

        public static string Convert(string cantidad)
        {
            var data = "";
            if (cantidad != null && !string.IsNullOrWhiteSpace(cantidad))
            {
                data = string.Format("{0:N}", decimal.Parse(cantidad));
            }
            else
            {
                data = "0.00";
            }

            return data;
        }

        public static async Task StamperPdfEstado(PdfStamper infor, List<PdfEstadoDto> data, datosPersona pdfEstado)
        {
            AcroFields fields = infor.AcroFields;

            fields.SetField("usuario", pdfEstado.Nombre + " " + pdfEstado.Apellidos);
            fields.SetField("cuenta", pdfEstado.NumTarjeta);
            fields.SetField("fechaDes", FechaFormatoddMMyyy(pdfEstado.FechaDescarga.ToString()));

            //recorrer transacciones list
            for(int i = 0; i < data.Count; i++)
            {
                var transaccion = data[i];
                fields.SetField($"Fecha{i+1}", FechaFormatoddMMyyy(transaccion.Fecha.ToString()));
                fields.SetField($"Detalle{i + 1}", transaccion.Descripcion);
                fields.SetField($"Monto{i + 1}", Convert(transaccion.Monto.ToString()));
                fields.SetField($"Saldo{i + 1}", Convert(transaccion.SaldoDisponible.ToString()));
            }

        }

    }
}

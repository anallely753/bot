using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace AhoraSi.Commons
{
    public class AttachmentCard
    {
        public static Activity GetFile(string tipoFormato)
        {
            var documentoCard = new Attachment();
            documentoCard.Name = $"Formato {tipoFormato}";
            documentoCard.ContentType = "application/pdf";
            switch (tipoFormato)
            {
                case "Vacaciones":
                    documentoCard.ContentUrl = "http://www.monterrey.gob.mx/pdf/new/Procedimientos/Administracion/P-SAD-REH-34SolicituddeVacaciones.pdf";
                    break;
                case "Permiso":
                    documentoCard.ContentUrl = "https://ucienegam.mx/wp-content/uploads/2018/08-Doc/Administracion/Formatos/FORMATOS_R.H..pdf";
                    break;
                case "Acceso":
                    documentoCard.ContentUrl = "https://www.ipomex.org.mx/recursos/ipo/files_ipo/2017/20/3/82269069831e5b2faf75b8799ca948b8.pdf";
                    break;
                case "Constancia laboral":
                    documentoCard.ContentUrl = "https://epe.upc.edu.pe/en/admission-epe/requirements/documentos/ejemplo_de_constancia_de_trabajo.pdf";
                    break;
            }

            var reply = MessageFactory.Attachment(documentoCard);
            return reply as Activity;
        }
    }
}

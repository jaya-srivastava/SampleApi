
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using iPractice.ViewModel;

namespace iPractice.Helpers
{
    public static class PDFGenerator
    {
        static string certTemplateName = ConfigurationManager.AppSettings["TemplatePath"];
        public static MemoryStream CreatePdf(CertificateVM certificate)
        {
            string templatePath = certTemplateName+certificate.TemplateName;
            if (certificate.CertificateID == null)
                return null;

            string pdfTemplate = HttpContext.Current.Server.MapPath(templatePath);
            MemoryStream workStream = new MemoryStream();
            using (PdfReader pdfReader = new PdfReader(pdfTemplate))
            {
                using (PdfStamper pdfStamper = new PdfStamper(pdfReader, workStream))
                {
                    AcroFields pdfFormFields = pdfStamper.AcroFields;
                    pdfFormFields.SetField("presentedTo", "Presented To");
                    pdfFormFields.SetField("userName", certificate.UserFirstName);
                    if (!certificate.SubTopicDesc.StartsWith("Percentage"))
                    pdfFormFields.SetField("for", "For");
                    pdfFormFields.SetField("subTopicDesc", certificate.SubTopicDesc);
                    pdfFormFields.SetField("doc",certificate.DateOfCompletion + "\n");
                    pdfFormFields.SetField("signatureBy", "");
                    pdfFormFields.SetField("companyName", "iPracticeMath.com");
                    pdfStamper.FormFlattening = true;
                }
            }
            return workStream;
        }
    }
}
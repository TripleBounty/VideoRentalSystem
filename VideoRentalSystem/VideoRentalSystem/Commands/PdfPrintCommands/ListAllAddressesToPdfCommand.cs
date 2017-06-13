using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllAddressesToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\AddressesList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Addresses Report";
        private readonly string header = "Addresses Report";
        private readonly string author = "Addresses Bounty";
        private readonly string target = "Addresses";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "Addresses LIST\n\n";
        private readonly string subTitle = "What Addresses are in the Trible Bounty system ?";
        private readonly string warningMessage = "All Addresses can be deleted without prior notice. Especially those with Mitko in them.";

        private readonly IDatabase db;

        public ListAllAddressesToPdfCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var objectsList = this.db.Addesses.GetAll();
            List<string> data = new List<string>();

            if (objectsList.Count == 0)
            {
                data.Add("No items");
            }

            foreach (var item in objectsList)
            {
                data.Add(item.ToString());
            }

            CreatePDF pdfCreator = new CreatePDF(
                                               this.fileName,
                                               this.imgPath,
                                               this.title,
                                               this.header,
                                               this.target,
                                               this.author,
                                               this.keyword,
                                               this.headerRental,
                                               this.listName,
                                               this.subTitle,
                                               this.warningMessage);

            pdfCreator.CreatePdf(data);

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }
    }
}
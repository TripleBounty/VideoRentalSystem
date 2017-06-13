using VideoRentalSystem.Commands.Contracts;
using System.Collections.Generic;
using VideoRentalSystem.Data.Postgre.Contracts;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllTarifsToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\TarifsList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Tarifs Report";
        private readonly string header = "Tarifs Report";
        private readonly string author = "Tarifs Bounty";
        private readonly string target = "Tarifs";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "Tarifs LIST\n\n";
        private readonly string subTitle = "What are the Tarifs in the Trible Bounty system ?";
        private readonly string warningMessage = "All Tarifs can be dropped without prior notice.";

        private readonly IDatabasePostgre db;

        public ListAllTarifsToPdfCommand(IDatabasePostgre db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var objectsList = this.db.Tarifs.GetAll();
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
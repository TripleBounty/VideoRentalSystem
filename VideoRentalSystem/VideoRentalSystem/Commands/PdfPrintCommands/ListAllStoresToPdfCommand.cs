using iTextSharp.text;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllStoresToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\StoresList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Stores Report";
        private readonly string header = "Stores Report";
        private readonly string target = "Stores";
        private readonly string author = "Triple Bounty";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "Stores LIST\n\n";
        private readonly string subTitle = "What Stores do we have ?";
        private readonly string warningMessage = "All Stores can be dropped with a two weeks prior notice. Especially Lamer once.";

        private readonly IDatabase db;

        public ListAllStoresToPdfCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var objectsList = this.db.Stores.GetAll();
            List<string> data = new List<string>();

            if (objectsList.Count == 0)
            {
                data.Add("No items");
            }

            foreach (var item in objectsList)
            {
                data.Add(item.ToString());
            }

            CreatePDF pdfCreator = new CreatePDF(fileName, imgPath, title, header, target, author, keyword, headerRental, listName,
                    subTitle, warningMessage);

            pdfCreator.CreatePdf(data);

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }

    }
}
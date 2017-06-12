using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllCountriesToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\CountriesList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Countries Report";
        private readonly string header = "Countries Report";
        private readonly string author = "Countries Bounty";
        private readonly string target = "Countries";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "Countries LIST\n\n";
        private readonly string subTitle = "What Countries are in the Trible Bounty system ?";
        private readonly string warningMessage = "All Countries can be deleted without prior notice. Especially those with Mitko in them.";

        private readonly IDatabase db;

        public ListAllCountriesToPdfCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var objectsList = this.db.Countries.GetAll();
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
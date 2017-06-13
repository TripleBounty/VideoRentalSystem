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
        private readonly CreatePDF pdf;

        public ListAllCountriesToPdfCommand(IDatabase db, CreatePDF pdf)
        {
            this.db = db;
            this.pdf = pdf;
        }

        public void AllObjectsToStringList(List<string> data)
        {
            var objectsList = this.db.Countries.GetAll();
            if (objectsList.Count == 0)
            {
                data.Add("No items");
            }

            foreach (var item in objectsList)
            {
                data.Add(item.ToString());
            }
        }

        public string Execute(IList<string> parameters)
        {
            List<string> data = new List<string>();
            this.AllObjectsToStringList(data);

            this.pdf.CreatePdf(
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
                this.warningMessage,
                data);

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }
    }
}
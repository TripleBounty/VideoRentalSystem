using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllCustomersToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\CustomersList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Customers Report";
        private readonly string header = "customers Report";
        private readonly string author = "Triple Bounty";
        private readonly string target = "customers";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "CUSTOMERS LIST\n\n";
        private readonly string subTitle = "Who is shopping in the Trible Bounty system ?";
        private readonly string warningMessage = "All customers can be banned without prior notice. Especially Mitko1.";

        private readonly IDatabase db;

        public ListAllCustomersToPdfCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var objectsList = this.db.Customers.GetAll();
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
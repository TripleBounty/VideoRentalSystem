using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllReviewsToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\ReviewsList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Reviews Report";
        private readonly string header = "Reviews Report";
        private readonly string author = "Triple Bounty";
        private readonly string target = "reviews";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "REVIEWS LIST\n\n";
        private readonly string subTitle = "What people think about the moves in the Trible Bounty system ?";
        private readonly string warningMessage = "All reviews can be deleted without prior notice. Especially those from Mitko1.";

        private readonly IDatabase db;
        private readonly CreatePDF pdf;

        public ListAllReviewsToPdfCommand(IDatabase db, CreatePDF pdf)
        {
            this.db = db;
            this.pdf = pdf;
        }

        public void AllObjectsToStringList(List<string> data)
        {
            var objectsList = this.db.Reviews.GetAll();
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
            AllObjectsToStringList(data);

            pdf.CreatePdf(fileName, imgPath, title, header, target, author, keyword, headerRental, listName,
                    subTitle, warningMessage, data);

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }
    }
}
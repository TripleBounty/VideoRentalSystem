using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using System.Collections.Generic;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class ListAllEmployeesToPdfCommand : ICommand
    {
        private readonly string fileName = @"..\..\..\EmployeesList.pdf";
        private readonly string imgPath = @"..\..\..\PaperAirplane.jpg";
        private readonly string title = "Employees Report";
        private readonly string header = "employees Report";
        private readonly string target = "employees";
        private readonly string author = "Triple Bounty";
        private readonly string keyword = "video rental system";
        private readonly string headerRental = "video rental";
        private readonly string listName = "EMPLOYEES LIST\n\n";
        private readonly string subTitle = "Who is working in the Trible Bounty headquarters ?";
        private readonly string warningMessage = "All employees can be fired with a two weeks prior notice. Especially Mitko1.";

        private readonly IDatabase db;
        
        public ListAllEmployeesToPdfCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var employeesList = this.db.Employees.GetAll();
            List<string> data = new List<string>();

            if(employeesList.Count == 0)
            {
                data.Add("No items");
            }

            foreach (var item in employeesList)
            {
                data.Add(item.ToString());
            }

            CreatePDF pdfCreator = new CreatePDF(fileName,imgPath, title, header, target, author, keyword, headerRental, listName,
                    subTitle, warningMessage);

            pdfCreator.CreatePdf(data);

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }

    }
}
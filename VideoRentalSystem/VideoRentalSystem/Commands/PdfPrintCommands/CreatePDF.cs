using iTextSharp.text;
using System;
using System.Collections.Generic;
using VideoRentalSystem.Common;

namespace VideoRentalSystem.Commands.PdfPrintCommands
{
    public class CreatePDF
    {
        private string fileName;
        private string imgPath;
        private string title;
        private string header;
        private string target;
        private string author;
        private string keyword;
        private string headerRental;
        private string listName;
        private string subTitle;
        private string warningMessage;

        //setuping fonts
        private iTextSharp.text.Font _largeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        private iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        private iTextSharp.text.Font _standardLink = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLUE);
        private iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        private iTextSharp.text.Font _linkFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLUE);

        public CreatePDF(string fileName, string imgPath, string title, string header, string target, string author, string keyword, string headerRental, string listName,
                    string subTitle, string warningMessage)
        {
            this.fileName = fileName;
            this.imgPath = imgPath;
            this.title = title;
            this.header = header;
            this.target = target;
            this.author = author;
            this.keyword = keyword;
            this.headerRental = headerRental;
            this.listName = listName;
            this.subTitle = subTitle;
            this.warningMessage = warningMessage;
        }

        public string CreatePdf(List<string> data)
        {
            iTextSharp.text.Document doc = null;

            try
            {
                // Initialize the PDF document
                doc = new Document();
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + fileName,
                        System.IO.FileMode.Create));

                // Set the margins and page size
                this.SetStandardPageSize(doc);

                // Add metadata to the document.  This information is visible when viewing the 
                // document properities within Adobe Reader.
                doc.AddTitle(title);
                doc.AddHeader("title", header);
                doc.AddHeader("author", author);
                doc.AddCreator(author);
                doc.AddKeywords(keyword);
                doc.AddHeader("subject", headerRental);

                // Add Xmp metadata to the document.
                this.CreateXmpMetadata(writer);

                // Open the document for writing content
                doc.Open();

                // Add pages to the document
                this.AddPageWithBasicFormatting(doc);
                this.AddPageWithBulletList(doc, data);

                // Add a final page
                this.SetStandardPageSize(doc);  // Reset the margins and page size
                this.AddPageWithExternalLinks(doc);

                // Add page labels to the document
                iTextSharp.text.pdf.PdfPageLabels pdfPageLabels = new iTextSharp.text.pdf.PdfPageLabels();
                pdfPageLabels.AddPageLabel(1, iTextSharp.text.pdf.PdfPageLabels.EMPTY, "Basic Formatting");
                pdfPageLabels.AddPageLabel(2, iTextSharp.text.pdf.PdfPageLabels.EMPTY, "Bullet List");
                pdfPageLabels.AddPageLabel(3, iTextSharp.text.pdf.PdfPageLabels.EMPTY, "External Links");
                writer.PageLabels = pdfPageLabels;
            }
            catch (iTextSharp.text.DocumentException dex)
            {
                throw new ArgumentException(dex.ToString());
            }
            finally
            {
                // Clean up
                doc.Close();
                doc = null;
            }

            return $"Pdf - {fileName} - with the list of all {target} was created in the project folder";
        }

        /// <summary>
        /// Add the header page to the document.  This shows an example of a page containing
        /// both text and images.  The contents of the page are centered and the text is of
        /// various sizes.
        /// </summary>
        /// <param name="doc"></param>
        private void AddPageWithBasicFormatting(iTextSharp.text.Document doc)
        {
            // Write page content.  Note the use of fonts and alignment attributes.
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new iTextSharp.text.Chunk("\n\n"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("VIDEO RENTAL SYSTEM\n\n"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _standardFont, new Chunk("by Tripple Bounty"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("\n\n\n"));

            // Add a logo
            string appPath = System.IO.Directory.GetCurrentDirectory();
            iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(appPath + imgPath);
            logoImage.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            doc.Add(logoImage);
            logoImage = null;

            // Write additional page content
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("\n\n\n"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk(subTitle));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("\n\n\n\n\n"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _smallFont, new Chunk("Generated " +
                TimeProvider.Current.UtcNow.Day.ToString() + " " +
                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(TimeProvider.Current.UtcNow.Month) + " " +
                TimeProvider.Current.UtcNow.Year.ToString() + " " +
                TimeProvider.Current.UtcNow.ToShortTimeString()));
        }

        /// <summary>
        /// Add a page that includes a bullet list.
        /// </summary>
        /// <param name="doc"></param>
        private void AddPageWithBulletList(iTextSharp.text.Document doc, List<string> data)
        {
            // Add a new page to the document
            doc.NewPage();

            // The header at the top of the page is an anchor linked to by the table of contents.
            iTextSharp.text.Anchor contentsAnchor = new iTextSharp.text.Anchor(listName, _largeFont);
            contentsAnchor.Name = "research";

            // Add the header anchor to the page
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, contentsAnchor);


            //string employeeList = string.Join("\n", employees);

            // Create an unordered bullet list.  The 10f argument separates the bullet from the text by 10 points
            iTextSharp.text.List list = new iTextSharp.text.List(iTextSharp.text.List.UNORDERED, 10f);
            list.SetListSymbol("\u2022");   // Set the bullet symbol (without this a hypen starts each list item)
            list.IndentationLeft = 20f;     // Indent the list 20 points

            foreach (var emp in data)
            {
                list.Add(new ListItem(emp, _standardFont));
            }

            doc.Add(list);  // Add the list to the page

            // Add some white space and another heading
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("\n\n\n"));
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("WARNING (!)\n\n"));

            // Add some final text to the page
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, _standardFont, new Chunk(warningMessage));
        }

        /// <summary>
        /// Add a page that contains embedded hyperlinks to external resources
        /// </summary>
        /// <param name="doc"></param>
        private void AddPageWithExternalLinks(Document doc)
        {
            // Generate external links to be embedded in the page
            iTextSharp.text.Anchor bibliographyAnchor1 = new Anchor("Click here to explore our project in GitHub", _standardFont);
            bibliographyAnchor1.Reference = "https://github.com/TripleBounty/VideoRentalSystem";

            // The header at the top of the page is an anchor linked to by the table of contents.
            iTextSharp.text.Anchor contentsAnchor = new iTextSharp.text.Anchor("\n\n", _largeFont);
            contentsAnchor.Name = "results";

            // Add a new page to the document
            doc.NewPage();

            // Add text to the page
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, contentsAnchor);
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, _largeFont, new Chunk("BIBLIOGRAPHY\n\n"));

            // Add the links to the page
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, _standardFont, bibliographyAnchor1);
        }

        /// <summary>
        /// Set margins and page size for the document
        /// </summary>
        /// <param name="doc"></param>
        private void SetStandardPageSize(iTextSharp.text.Document doc)
        {
            // Set margins and page size for the document
            doc.SetMargins(50, 50, 50, 50);
            // There are a huge number of possible page sizes, including such sizes as
            // EXECUTIVE, POSTCARD, LEDGER, LEGAL, LETTER_LANDSCAPE, and NOTE
            doc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.LETTER.Width,
                iTextSharp.text.PageSize.LETTER.Height));
        }

        /// <summary>
        /// Add a paragraph object containing the specified element to the PDF document.
        /// </summary>
        /// <param name="doc">Document to which to add the paragraph.</param>
        /// <param name="alignment">Alignment of the paragraph.</param>
        /// <param name="font">Font to assign to the paragraph.</param>
        /// <param name="content">Object that is the content of the paragraph.</param>
        private void AddParagraph(Document doc, int alignment, iTextSharp.text.Font font, iTextSharp.text.IElement content)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.SetLeading(0f, 1.2f);
            paragraph.Alignment = alignment;
            paragraph.Font = font;
            paragraph.Add(content);
            doc.Add(paragraph);
        }

        /// <summary>
        /// Use this method to write XMP data to a new PDF
        /// </summary>
        /// <param name="writer"></param>
        private void CreateXmpMetadata(iTextSharp.text.pdf.PdfWriter writer)
        {
            // Set up the buffer to hold the XMP metadata
            byte[] buffer = new byte[65536];
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer, true);

            try
            {
                // XMP supports a number of different schemas, which are made available by iTextSharp.
                // Here, the Dublin Core schema is chosen.
                iTextSharp.text.xml.xmp.XmpSchema dc = new iTextSharp.text.xml.xmp.DublinCoreSchema();

                // Add Dublin Core attributes
                iTextSharp.text.xml.xmp.LangAlt title = new iTextSharp.text.xml.xmp.LangAlt();
                title.Add("x-default", "Video rental system by Tripple Bounty");
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.TITLE, title);

                // Dublin Core allows multiple authors, so we create an XmpArray to hold the values
                iTextSharp.text.xml.xmp.XmpArray author = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.ORDERED);
                author.Add("Tripple bounty");
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.CREATOR, author);

                // Multiple subjects are also possible, so another XmpArray is used
                iTextSharp.text.xml.xmp.XmpArray subject = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.UNORDERED);
                subject.Add("Video casettes");
                subject.Add("Video rental system");
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.SUBJECT, subject);

                // Create an XmpWriter using the MemoryStream defined earlier
                iTextSharp.text.xml.xmp.XmpWriter xmp = new iTextSharp.text.xml.xmp.XmpWriter(ms);
                xmp.AddRdfDescription(dc);  // Add the completed metadata definition to the XmpWriter
                xmp.Close();    // This flushes the XMP metadata into the buffer

                //---------------------------------------------------------------------------------
                // Shrink the buffer to the correct size (discard empty elements of the byte array)
                int bufsize = buffer.Length;
                int bufcount = 0;
                foreach (byte b in buffer)
                {
                    if (b == 0) { break; }
                    bufcount++;
                }
                System.IO.MemoryStream ms2 = new System.IO.MemoryStream(buffer, 0, bufcount);
                buffer = ms2.ToArray();
                //---------------------------------------------------------------------------------

                // Add all of the XMP metadata to the PDF doc that we're building
                writer.XmpMetadata = buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }
    }
}

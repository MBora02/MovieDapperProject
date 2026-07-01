using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieDapperProject.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MovieDapperProject.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View(Context.Listeleme<GenresModel>("sp_GenreViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@GenreId", id);

                return View(
                    Context.Listeleme<GenresModel>("sp_GenreViewById", param)
                    .FirstOrDefault()
                );
            }
        }

        [HttpPost]
        public IActionResult EY(GenresModel genre)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@GenreId", genre.Id);
            param.Add("@Name", genre.Name);

            Context.ExecuteReturn("sp_GenreEY", param);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@GenreId", id);

            Context.ExecuteReturn("sp_GenreSil", param);

            return RedirectToAction("Index");
        }


        public IActionResult ExportToPdf()
        {
            var genres = Context.Listeleme<GenresModel>("sp_GenreViewAll").ToList();

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header()
                        .Text("Film Türleri Raporu")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Tür Adı").Bold();
                            });

                            foreach (var item in genres)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Id.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Name ?? "");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfBytes = pdfDocument.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Tur_Listesi_{DateTime.Now:yyyyMMdd}.pdf");
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Backend softito");

            var genres = Context.Listeleme<GenresModel>("sp_GenreViewAll").ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Tür Listesi");

                worksheet.Cells[1, 1].Value = "Tür ID";
                worksheet.Cells[1, 2].Value = "Tür Adı";

                using (var range = worksheet.Cells[1, 1, 1, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(41, 128, 185));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int rowNumber = 2;
                foreach (var item in genres)
                {
                    worksheet.Cells[rowNumber, 1].Value = item.Id;
                    worksheet.Cells[rowNumber, 2].Value = item.Name;
                    rowNumber++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                string fileName = $"Tur_Listesi_{DateTime.Now:yyyyMMdd}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}

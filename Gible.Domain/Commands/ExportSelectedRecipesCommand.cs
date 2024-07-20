using Gible.Domain.Settings;
using Gible.Tech.PDF;
using Knox.Commanding;

namespace Gible.Domain.Commands
{
    public record ExportSelectedRecipesCommand(string FileName, IEnumerable<string> FilePaths) : Command;
    public class ExportSelectedRecipesCommandHandler(IPdfExporter pdfExporter, IApplicationSettings applicationSettings) : CommandHandler<ExportSelectedRecipesCommand>
    {
        protected override Task<bool> InternalCanExecuteAsync(ExportSelectedRecipesCommand command)
        {
            throw new NotImplementedException();
        }

        protected override Task InternalExecuteAsync(ExportSelectedRecipesCommand command)
        {
            pdfExporter.ExportPdf(applicationSettings.BaseDirectory, command.FileName, command.FilePaths);
            return Task.CompletedTask;
        }
    }
}

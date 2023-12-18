using Gible.Domain.Settings;
using Gible.Tech.PDF;
using Knox.Commanding;

namespace Gible.Domain.Commands
{
    public record ExportSelectedRecipesCommand(string FileName, IEnumerable<string> FilePaths) : Command;
    public class ExportSelectedRecipesCommandHandler(IPdfExporter pdfExporter, IApplicationSettings applicationSettings) : ICommandHandler<ExportSelectedRecipesCommand>
    {
        public Task<bool> CanExecuteAsync(ExportSelectedRecipesCommand command)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(ExportSelectedRecipesCommand command)
        {
            pdfExporter.ExportPdf(applicationSettings.BaseDirectory, command.FileName, command.FilePaths);
            return Task.CompletedTask;
        }
    }
}

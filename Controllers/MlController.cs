using EstatePredict.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ml")]
public class MlController : ControllerBase
{
    private readonly IPropertyExportService _exportService;

    public MlController(IPropertyExportService exportService)
    {
        _exportService = exportService;
    }

    [HttpGet("export-properties")]
    public async Task<IActionResult> ExportProperties()
    {
        var fileBytes = await _exportService.ExportPropertiesToCsvAsync();

        return File(
            fileBytes,
            "text/csv",
            "properties.csv"
        );
    }
}
namespace EstatePredict.Interfaces
{
    public interface IPropertyExportService
    {
        Task<byte[]> ExportPropertiesToCsvAsync();
    }
}

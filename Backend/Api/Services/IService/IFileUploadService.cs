namespace Api.Services.IService;

public interface IFileUploadService
{
    Task<List<string>> UploadFile(IFormFile file, string folder);
    bool DeleteFile(string filePath);
}

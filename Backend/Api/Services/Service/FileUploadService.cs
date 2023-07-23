using Api.Services.IService;

namespace Api.Services.Service;

public class FileUploadService : IFileUploadService
{
    private readonly IHostEnvironment _hostEnvironment;

    public FileUploadService(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    public bool DeleteFile(string filePath)
    {
        if (File.Exists(_hostEnvironment.ContentRootPath + filePath))
        {
            File.Delete(_hostEnvironment.ContentRootPath + filePath);
            return true;
        }
        return false;
    }

    public async Task<List<string>> UploadFile(IFormFile file, string folder)
    {
        FileInfo fileInfo = new(file.FileName);
        string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
        string folderDirectory = $"{_hostEnvironment.ContentRootPath}//profileImages//{folder}";
        if (!Directory.Exists(folderDirectory))
        {
            Directory.CreateDirectory(folderDirectory);
        }
        string filePath = Path.Combine(folderDirectory, fileName);
        await using FileStream fs = new FileStream(filePath, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs);
        string fullPath = $"/profileImages/{folder}/{fileName}";
        List<string> result = new List<string> { fileName, fullPath };
        return result;
    }
}

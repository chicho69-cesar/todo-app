namespace TodoApp.Services.Interfaces {
    public interface IBlobService {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);
        Task<Guid> UploadBlobAsync(byte[] file, string containerName);
        Task<Guid> UploadBlobAsync(string image, string containerName);
        Task DeleteBlobAsync(Guid id, string containerName);
    }
}
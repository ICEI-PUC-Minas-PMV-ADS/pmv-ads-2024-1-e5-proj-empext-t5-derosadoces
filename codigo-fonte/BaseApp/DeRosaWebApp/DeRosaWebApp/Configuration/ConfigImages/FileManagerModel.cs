namespace DeRosaWebApp.Configuration.ConfigImages
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile _IFormFile { get; set; }
        public List<IFormFile> _IFormFiles { get; set; }
        public string PathImagesProduto { get; set; }
    }
}

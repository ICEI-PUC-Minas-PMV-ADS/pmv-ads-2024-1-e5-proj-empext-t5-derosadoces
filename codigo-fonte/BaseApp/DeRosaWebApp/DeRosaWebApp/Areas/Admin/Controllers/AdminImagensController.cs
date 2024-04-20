using DeRosaWebApp.Configuration.ConfigImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminImagensController(IWebHostEnvironment hostingEnvironment, IOptions<ConfigurationImages> myConfiguration)
        {
            _myConfig = myConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("{controller}")]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [Route("{controller}/UploadFiles/files")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Erro: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }
            if (files.Count > 50)
            {
                ViewData["Erro"] = "Erro: Quantidade de arquivos excedeu o limite (10)";
                return View(ViewData);
            }
            long size = files.Sum(f => f.Length);
            var filePathsName = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathsName.Add(fileNameWithPath);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " + $"com tamanho total de: {size} bytes";
            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }
        [Route("{controller}/GetImagens")]
        public IActionResult GetImagens()
        {

            FileManagerModel fileManagerModel = new FileManagerModel();

            var usersImagensPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(usersImagensPath);

            FileInfo[] files = dir.GetFiles();

            fileManagerModel.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {usersImagensPath}";
            }
            fileManagerModel.Files = files;
            return View(fileManagerModel);
        }
        [Route("{controller}/DeleteFile")]
        public IActionResult DeleteFile(string fname)
        {
            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos + "\\", fname);
            if ((System.IO.File.Exists(imagePath)))
            {
                System.IO.File.Delete(imagePath);
                ViewData["Deletado"] = $"Arquivo(s) {imagePath} deletado com sucesso!";

            }
            return View("Index");
        }
    }
}
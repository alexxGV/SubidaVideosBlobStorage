[AuthorizeUsers]
public IActionResult Create()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> Create
    (String titulo, int idgenero, String sinopsis, int valoracion, int duracion,
    IFormFile fileImagen, IFormFile fileVideo)
{

    //await this.UploadService.UploadFileAsync(fileImagen, Folders.Images);
    //await this.UploadService.UploadFileAsync(fileVideo, Folders.Videos);

    String blobImagenName = fileImagen.FileName;
    using (var stream = fileImagen.OpenReadStream())
    {
        await this.ServiceBlobs.UploadBlobAsync(Folders.Images, blobImagenName, stream);
    }

    String blobVideoName = fileVideo.FileName;
    using (var stream = fileVideo.OpenReadStream())
    {
        await this.ServiceBlobs.UploadBlobAsync(Folders.Videos, blobVideoName, stream);
    }

    await this.service.InsertPelicula(titulo, idgenero, sinopsis, valoracion, duracion, 
        fileImagen.FileName, fileVideo.FileName);
    return RedirectToAction("List");
}

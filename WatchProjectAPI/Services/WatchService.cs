using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Globalization;
using WatchProjectAPI.Model;
using WatchProjectAPI.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace WatchProjectAPI.Services
{
    public class WatchService : IWatchService
    {

        private readonly IWatchRepository _watchRepository;
        private readonly FileSettings _fileSettings;


        public WatchService(IWatchRepository watchRepository, IWebHostEnvironment webHostEnvironment, IOptionsMonitor<FileSettings> optionsMonitor)
        {
            _watchRepository = watchRepository;
            _fileSettings = optionsMonitor.CurrentValue;
        }


        public async Task<Tuple<bool, string>> SaveData(Watch model)
        {

            var newFileName = Path.Combine(_fileSettings.Directory!, model.ImageUrl!);


            var extension = Path.GetExtension(newFileName);

            model.Id = Guid.NewGuid();
            model.ImageUrl = $"{model.Id}{extension}";

            await _watchRepository.SaveData(model);


            SaveImagetoFolder(model.WatchImage!, model.ImageUrl);

            return Tuple.Create(true, "Save watch completed");

        }

        private void SaveImagetoFolder(byte[] img, string filename)
        {
            try
            {
                if (img != null)
                {
                    var filextension = Path.GetExtension(filename);

                    using MemoryStream fileUploadstream = new MemoryStream(img);

                    BlobContainerClient client = new BlobContainerClient(_fileSettings.azureConnectionString, _fileSettings.Container);

                    BlobClient file = client.GetBlobClient(filename);

                    file.Upload(fileUploadstream, new BlobUploadOptions()
                    {
                        HttpHeaders = new BlobHttpHeaders
                        {
                            ContentType = getContentType(filextension)
                        }
                    }, cancellationToken: default);
                }

            }
            catch (Exception ex) { throw ex; }

        }


        private string getContentType(string extensionfile)
        {
            string contentType = string.Empty;

            if (extensionfile.ToUpper() == ".PNG")
            {
                contentType = "image/png";
            }
            else if (extensionfile.ToUpper() == ".JPG")
            {
                contentType = "image/jpg";
            }
            return contentType;
        }


        public async Task<IEnumerable<Watch>> GetAll(string?watchName)
        {
            try
            {
                if (watchName != null)
                {
                    var datasearch = await _watchRepository.GetbyName(watchName);
                    datasearch.ToList().ForEach(x => x.ImageUrl = $"{_fileSettings.Directory}/{x.ImageUrl}");
                    return datasearch.ToList();
                }
                else
                {
                    var data = await _watchRepository.GetAll();
                    data.ToList().ForEach(x => x.ImageUrl = $"{_fileSettings.Directory}/{x.ImageUrl}");
                    return data.ToList();
                }

            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IEnumerable<Watch>> GetRandom()
        {
            var data = await _watchRepository.GetRandom();
            data.ToList().ForEach(x => x.ImageUrl = $"{_fileSettings.Directory}/{x.ImageUrl}");
            return data.ToList();
        }

        public async Task<IEnumerable<Watch>> GetRandom8()
        {
            var data = await _watchRepository.GetRandom8();
            data.ToList().ForEach(x => x.ImageUrl = $"{_fileSettings.Directory}/{x.ImageUrl}");
            return data.ToList();
        }

        public async Task<Watch> GetbyId(Guid id)
        {
            var model = await _watchRepository.GetbyId(id);
            model.ImageUrl = $"{_fileSettings.Directory}/{model.ImageUrl}";

            return model;
        }

        public async Task Update(Watch model)
        {
            try
            {
                    var oldFileName = Path.Combine(_fileSettings.Directory!, model.ImageUrl!);
                    var extension = Path.GetExtension(oldFileName);
                    model.ImageUrl = $"{model.Id}{extension}";
                    await _watchRepository.Update(model);
                    SaveImagetoFolder(model.WatchImage!, model.ImageUrl!);
                
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task Delete(Guid id)
        {
            var model = await _watchRepository.GetbyId(id) ?? throw new Exception();
            await _watchRepository.Delete(id);

            await DeleteImage(model.ImageUrl!);
        }

        public async Task DeleteImage(string image)
        {
            BlobContainerClient client = new BlobContainerClient(_fileSettings.azureConnectionString, _fileSettings.Container);
            BlobClient file = client.GetBlobClient(image);
            try
            {
                if (file.Exists())
                { await file.DeleteAsync(); }

            }
            catch (Exception ex) { throw ex; }
        }


    
    }
}



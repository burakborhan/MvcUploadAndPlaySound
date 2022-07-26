using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSesYüklemeGüncel.Entity.EntityModel;
using MvcSesYüklemeGüncel.Entity.EntityDatabase;
using MvcSesYüklemeGüncel.Entity.EntityUnitOfWork;

namespace MvcSesYüklemeGüncel.Controllers
{

    public class HomeController : Controller
    {
        private SesContext _sesContext;
        private IUnitOfWork _uow;
        private IRepository<AudioFile> _audioFile;

        public HomeController()
        {
            _sesContext = new SesContext();
            _uow = new UnitOfWork(_sesContext);
            _audioFile = new Repository<AudioFile>(_sesContext);
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadAudio()
        {
            List<AudioFile> SesKayitlari = _audioFile.ListQueryable().ToList();
            return View(SesKayitlari);
        }
        [HttpPost]
        public ActionResult UploadAudio(AudioFile audio, HttpPostedFileBase fileupload)
        {
            string dosyaAdi = "";


            if (fileupload != null)
            {
                FileInfo dosyaBilgisi = new FileInfo(fileupload.FileName);
                audio.Name = fileupload.FileName;
                string yuklemeYeri = "Folder" + "/" + "Sozlesme";
                string dizinAdi = Server.MapPath("~/" + yuklemeYeri);
                if (Directory.Exists(dizinAdi))
                {
                    dosyaAdi = audio.Name + dosyaBilgisi.Extension;
                    string dosyaYeri = Server.MapPath("~/" + yuklemeYeri + "/" + dosyaAdi);
                    audio.FileSize = fileupload.ContentLength/1000;
                    audio.FilePath = yuklemeYeri + "/" + dosyaAdi;
                    fileupload.SaveAs(dosyaYeri);

                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/" + yuklemeYeri));
                    dosyaAdi = audio.Name + dosyaBilgisi.Extension;
                    string dosyaYeri = Server.MapPath("~/" + yuklemeYeri + "/" + dosyaAdi);
                    audio.FilePath = yuklemeYeri + "/" + dosyaAdi;
                    audio.FileSize = fileupload.ContentLength / 1000;
                    fileupload.SaveAs(dosyaYeri);
                }
              
               
                    audio.Name = dosyaAdi;
                    _audioFile.Insert(audio);
                    _uow.SaveChanges();
                    return RedirectToAction("UploadAudio");
                
                

            }

            List<AudioFile> SesKayitlari = _audioFile.ListQueryable().ToList();
            return View(SesKayitlari);

            
    }
}
    }

    

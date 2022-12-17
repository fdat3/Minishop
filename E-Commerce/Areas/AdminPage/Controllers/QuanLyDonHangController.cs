using E_Commerce.Models;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: AdminPage/QuanLyDonHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChuaThanhToan()
        {
            var lstChuaTT = db.DonDatHangs.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgayDatHang);
            return View(lstChuaTT);
        }

        public ActionResult ChuaGiao()
        {
            var lstChuaGiao = db.DonDatHangs.Where(n => n.TinhTrangDH == false).OrderBy(n => n.NgayDatHang);
            return View(lstChuaGiao);
        }

        public ActionResult DaGiaoDaTT()
        {
            var lstDone = db.DonDatHangs.Where(n => n.DaThanhToan == true && n.TinhTrangDH == true);
            return View(lstDone);
        }
        [HttpGet]
        public ActionResult DuyetDH(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);

            if (ddh == null)
            {
                return HttpNotFound();
            }

            var lstChiTiet = db.ChiTietDonDatHangs.Where(n => n.MaDDH == id);
            ViewBag.lstChiTiet = lstChiTiet;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult DuyetDH(DonDatHang ddh)
        {
            //update lai 
            DonDatHang ddhUpdate = db.DonDatHangs.Single(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangDH = ddh.TinhTrangDH;
            ddhUpdate.NgayGiao = DateTime.Now;
            db.SaveChanges();

            var lstChiTiet = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.lstChiTiet = lstChiTiet;

            //send Mail
            sendMail("Xác nhận đơn hàng của hệ thống miniShop !", "datp0628@gmail.com", "2100010224@nttu.edu.vn", "Nttu@03062003", "Đơn hàng của bạn đã được giao ! Xin cám ơn bạn đã ủng hộ sản phẩm của chúng tôi !");

            return View(ddhUpdate);
        }

        public ActionResult exportPdf()
        {
            return new ActionAsPdf("DuyetDH")
            {
                FileName = Server.MapPath("~/Content/DuyetDH.pdf")
            };
        }

        public void sendMail(string title, string toEmail, string fromEmail, string password, string content)
        {
            //call email
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(toEmail);
            mail.Subject = title;
            mail.Body = content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);  
        }
    }
}
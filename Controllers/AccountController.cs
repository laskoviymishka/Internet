using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Internet.Models;
using System.Net.Mail;
using System.Net;


namespace Internet.Controllers
{
    public class AccountController : Controller
    {
        #region DefaultUserLogic
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Логин или пароль неверны.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
       
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("SelectRole", "Account");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword
        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный пароль.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion
        [Authorize]
        public ActionResult SelectRole()
        {
            return View();
        }

      
        [Authorize]
        [HttpPost]
        public ActionResult SelectRole(FormCollection item)
        {
            try
            {
                Roles.AddUserToRole(User.Identity.Name, item.GetValues("Role").First());
            }
            catch (System.Configuration.Provider.ProviderException) 
            {
                return RedirectToAction("Index", "Home");
            }
          
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(ForgotPass item)
        {
            MembershipUser currentUser = Membership.GetUser(Membership.GetUserNameByEmail(item.Email));
            string newPassword = currentUser.ResetPassword();
            ViewData["T"] = newPassword;
            SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 587);
            Smtp.Credentials = new NetworkCredential("shahterv", "4321");
            

            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("shahterv@yandex.ru");
            Message.To.Add(new MailAddress(item.Email));
            Message.Subject = "Заголовок";
            Message.Body = "Ваш новый пароль - " + newPassword;
            Smtp.Send(Message);
            return View();
        }
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Логин занят. Введите другой логин.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "e-mail уже используется. Пожалуйста введите друго e-mail адресс.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Пароль неверный. Введите верный пароль.";

                case MembershipCreateStatus.InvalidEmail:
                    return "e-mail неверный. Введите корректный адресс.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Не корректный логин. Введите корректный логин.";

                case MembershipCreateStatus.ProviderError:
                    return "Вход не был осуществлен. Попробуйте заново, если ошибка повторится свяжитесь с администратором.";

                case MembershipCreateStatus.UserRejected:
                    return "Пользователь не был создан. Попробуйте заново, если ошибка повторится свяжитесь с администратором.";

                default:
                    return "Какой-то звиздец случился, я даже не знаю что это блин такое.";
            }
        }
        #endregion
    }
}

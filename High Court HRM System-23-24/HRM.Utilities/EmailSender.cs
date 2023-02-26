using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendConfirmationEmail(email,subject, htmlMessage);
            return Task.CompletedTask;
        }
        private void SendConfirmationEmail(string email, string subject,string htmlMessage)
        {
            try
            {
               
                //SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
                //con.Open();
                
            
                    
                    //if (mobilenumber != string.Empty)
                    //{
                    //    string message;
                    //    // message = "Dear Mr./Ms. " + FullName + ", " + " Your application has been submitted successfully in online job portal of LHC,Thank you." +DateTime.Now.ToShortDateString()+ "";
                    //    message = "Dear Mr./Ms. " + FullName + ", " + "You have submitted application for the post of " + _post.PostName + " with CNIC: " + CNIC + " on " + DateTime.Now.ToString("dd-MM-yyyy");
                    //    System.Net.WebClient webClient = new System.Net.WebClient();
                    //    string result = webClient.DownloadString("http://sys.lhc.gov.pk/api/send_sms_custom.php?number=" + mobilenumber + "&message=" + (Uri.EscapeDataString(message)));
                    //}

                
                    string pattern = null;
                    pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                    if (email!= "")
                    {
                        if (Regex.IsMatch(email, pattern))
                        {
                            //FullName = dt.Rows[0]["FullName"].ToString();
                          

                            try
                            {
#if DEBUG
                            String SendMailFrom = "rtraees@gmail.com";
                            String SendMailTo = email;
                        
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                            MailMessage emailmesage = new MailMessage();
                            // START
                            emailmesage.From = new MailAddress(SendMailFrom);
                            emailmesage.To.Add(SendMailTo);
                            emailmesage.CC.Add(SendMailFrom);
                            emailmesage.Subject = subject;
                            emailmesage.Body = htmlMessage;
                            //END
                            SmtpServer.Timeout = 15000;
                            SmtpServer.EnableSsl = true;
                            emailmesage.IsBodyHtml = true;
                            SmtpServer.UseDefaultCredentials = false;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(SendMailFrom, "hrmqnibbdjlmvgri");
                            SmtpServer.Send(emailmesage);

#else
      SmtpClient SMTPServer = new SmtpClient();
                    System.Net.Mail.MailMessage Mail = new System.Net.Mail.MailMessage();
                            Mail.From = new MailAddress("jobs@lhc.gov.pk", "LHC: AutoEmail Service");
                            Mail.To.Add(email);
                        Mail.Subject = subject;// "Congratulations: Your Online Forms Submitted for LHC Jobs";
                            Mail.IsBodyHtml = true;
                        //Mail.Body = "Dear " + FullName + ", " + " You have successfully submitted your on-line application for the post of  ADDITIONAL DISTRICT & SESSIONS JUDGE. against CNIC:" + CNIC + " <br /> <br /> NOTICE  All candidates are advised to visit Lahore High Court's website for any information and updates. In casse of any query they may contact 04299212951 (Ext: 329) and email at 'rec.adjs@lhc.gov.pk' Note *** this is an automatically generated e-mail.Please do not reply ***  Thank you. LAHORE HIGH COURT, LAHORE";
                        //SMTPServer.Credentials = new System.Net.NetworkCredential("jobs.lhc", "jobs@lhc123");
                        Mail.Body = htmlMessage;// "Dear " + FullName + ", " + "You have successfully submitted your online application for the post of <b>" + _post.PostName + "<b> against CNIC : " + CNIC + "<br/><b>Notice<b> <br/>All candidates are advised to visit Lahore High Court’s website for any information and updates. In case of any query you may contact 042-99212951 Ext: 329(for general query) Ext: 250(for technical query) and email at examination@lhc.gov.pk(for general query), jobs@lhc.gov.pk(for technical query) <br/><br/>Note: *** This is an automatically generated e-mail. Please do not reply*** <br/> <br/><br/>Thank you. LAHORE HIGH COURT, LAHORE";
                            SMTPServer.Credentials = new System.Net.NetworkCredential("jobs.lhc", "Honda@349");
                            SMTPServer.Port = 25;
                            //SMTPServer.Host = "smtprelay.punjab.gov.pk";
                            SMTPServer.Host = "10.50.124.135";
                            SMTPServer.Send(Mail);
#endif

                        }
                        catch (Exception ex)
                            {
                                //Response.Write("Failed To Send Email");
                                
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                //string pageName = (Page)HttpContext.Current.CurrentHandler.GetType().Name;
                //ExceptionAdapter.InsertQuery(CandidateId, pageName, ex.Message);
                // Response.Write("Failed To Send Email");
            }
        }
    }
}

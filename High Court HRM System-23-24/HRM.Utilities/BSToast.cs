using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    /// <summary>
    /// Boostrap Toaster
    /// </summary>
    public class BSToast
    {
        public string ID;
        public bool Animation { get; set; }
        public bool AutoHide { get; set; }
        public int DelayIn_ms { get; set; }
        public string Bg_Class { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Position { get; set; }
        
        /// <summary>
        /// Redirect to some page
        /// </summary>
        public string TakeAction { get; set; }

        public string TakeActionBg_Class { get; set; }
        public BSToast(string title, string message, ToastType type=ToastType.Default) {
            this.Title = title;
            this.Message=message;
            this.Animation = true;
            this.AutoHide = true;
            DelayIn_ms = 1500;
            Position = ToastPosition.Top_right;
            this.ID = "t_" + (new Random().Next(100, 1000));
        }
        public override string ToString()
        {
            string to = $@"<div aria-live='polite' aria-atomic='true' class='bg-dark position-relative bd-example-toasts'>
                              <div class='toast-container position-absolute {this.Position} p-3' id='toastPlacement'>
                                <div class='toast' id='{this.ID}'>
                                  <div class='toast-header'>
                                    <img src='...' class='rounded me-2' alt='...'>
                                    <strong class='me-auto'>{this.Title}</strong>
                                    <small>just now</small>
                                  </div>
                                  <div class='toast-body'>
                                    {this.Message}
                                  </div>
                                </div>
                              </div>
                            </div>";
            string x = "<script>jQuery(document).ready(function(){ jQuery('#" + this.ID + "').toast('show'); });</script>";
            

            return to+x;
        }
        private string getBgClass(ToastType t) {
            switch (t)
            {
                case ToastType.Default:
                    return "";
                    break;
                case ToastType.Danger:
                    return "bg-danger";
                    break;
                case ToastType.Primary:
                    return "bg-primary";
                    break;
                case ToastType.Warning:
                    return "bg-warning";
                    break;
                case ToastType.Dark:
                    return "bg-dark";
                    break;
                case ToastType.Secondary:
                    return "bg-secondary";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
    public enum ToastType
    { 
        Default,
        Danger,
        Primary,
        Warning,
        Dark,
        Secondary
    }
    public class ToastPosition
    {
      public static string Top_left="top-0 start-0";
      public static string Top_center="top-0 start-50 translate-middle-x";
      public static string Top_right="top-0 end-0";
      public static string Middle_left="top-50 start-0 translate-middle-y";
      public static string Middle_center="top-50 start-50 translate-middle";
      public static string Middle_right="top-50 end-0 translate-middle-y";
      public static string Bottom_left="bottom-0 start-0";
      public static string Bottom_center="bottom-0 start-50 translate-middle-x";
      public static string Bottom_right="bottom-0 end-0";
    }
    public class BSToastHelper
    {
        public static void Add(Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary TempData, BSToast bs)
        {
            if (TempData["BSToasts"] == null)
                TempData["BSToasts"] = new List<BSToast>() { bs };
            else
            {
                var t = (List<BSToast>)TempData["BSToasts"];
                t.Add(bs);
                TempData["BSToasts"] = t;
            }
            
        }
    }
}
